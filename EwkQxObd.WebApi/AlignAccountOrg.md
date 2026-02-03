# Align Account and Organization Tables

## 1. Situation Tally & Preparation

The Org table is the latest consolidated table for account and organization objects:

```sql
USE [EwkIqxOnboardingApi]
GO

/****** Object:  Table [iqx].[Org]    Script Date: 2/2/2026 3:03:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [iqx].[Org](
	[AccountNumber] [int] NOT NULL,
	[Name] [nvarchar](64) NULL,
	[GeisGuid] [uniqueidentifier] NULL,
	[Country] [nvarchar](16) NULL,
	[Region] [nvarchar](16) NULL,
	[City] [nvarchar](64) NULL,
	[Street] [nvarchar](128) NULL,
 CONSTRAINT [PK_Org] PRIMARY KEY CLUSTERED 
(
	[AccountNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

The below chart shows current status to `Vinkls` or "View of Instrument Link Status".

![image-20260202145436428](C:\Users\mochen\source\EwkQxObd\EwkQxObd.WebApi\AlignAccountOrg.md.tabledeps-1.png)

In this chart, the base table is `syngio_incl_co`, we shall try to re-bind the org object to it.

This table contains the below definition:

```sql
SELECT DISTINCT 
                         iqx.NetworkInstrument.id, iqx.NetworkInstrument.System, COALESCE (iqx.NetworkInstrument.QueryTimeStamp, N'2025-10-10T18:30:57') AS QueryTimeStamp, iqx.NetworkInstrument.NetworkName, 
                         iqx.NetworkInstrument.NetworkId, iqx.NetworkInstrument.LinkedAccount, iqx.NetworkInstrument.InstrumentGroup, COALESCE (iqx.NetworkInstrument.SerialNumber, Eqo.ContractObject.SerialNumber) AS SerialNumber, 
                         iqx.NetworkInstrument.InstrumentName, Eqo.Contract.ContractNumber, Eqo.ContractObject.Contract AS ContractId, Eqo.ContractObject.ShipTo
FROM            Eqo.Contract INNER JOIN
                         Eqo.ContractObject ON Eqo.Contract.Id = Eqo.ContractObject.Contract FULL OUTER JOIN
                         iqx.NetworkInstrument ON Eqo.ContractObject.SerialNumber = iqx.NetworkInstrument.SerialNumber
```

Surprisingly this table does not contain detailed org info. Just a reference as "ShipTo".

The base table that has organization info is `vwFlatContractObject`, we shall start here:

```sql
SELECT
    Eqo.Contract.ContractNumber,
    Eqo.Contract.Description,
    Eqo.Contract.ValidFrom,
    Eqo.Contract.ValidTo,
    Eqo.ContractObject.SerialNumber,
    Eqo.ContractObject.InstrumentType,
    Eqo.Account.PartnerId,
    Eqo.Account.PartnerName,
    Eqo.Account.GeisID,
    Eqo.Account.Country,
    Eqo.Account.Region,
    Eqo.Contract.RecordedAt,
    Eqo.ContractObject.Id AS ContractObjId,
    Eqo.ContractObject.IsSite
FROM
    Eqo.ContractObject
    LEFT OUTER JOIN Eqo.Account ON Eqo.ContractObject.ShipTo = Eqo.Account.Id
    LEFT OUTER JOIN Eqo.Contract ON Eqo.ContractObject.Contract = Eqo.Contract.Id
```

Before we can switch we need to consolidate a conflict: Contract Object has Account but was linked via "id", not account number.

```sql
SELECT
    c.ContractNumber,
    c.Description,
    c.ValidFrom,
    c.ValidTo,
    t.SerialNumber,
    t.InstrumentType,
    o.AccountNumber,
    o.Name,
    o.GeisGuid,
    o.Country,
    o.Region,
    c.RecordedAt,
    t.Id AS ContractObjId,
    t.IsSite
FROM
    Eqo.ContractObject t
    LEFT OUTER JOIN Iqx.Org o ON t.ShipTo = o.AccountNumber
    LEFT OUTER JOIN Eqo.Contract c ON t.Contract = c.Id
```

Completed.

Now check up for all deps with this view, next one is Flat Contract Object Site Only.

## 2. Consolidated Site Contracts

Site contracts are special contracts that rather than specify instruments, they specify "sites".

It has caused kind of a problem for the view to work, because the contract object operates only on the concept of "instrument - contract(organization) - connection status"

Current (faulty) site contract table definition is:

```
SELECT        TOP (1000) ContractNumber, Description, ValidFrom, ValidTo, SerialNumber, InstrumentType, PartnerId, PartnerName, GeisID, Country, Region, RecordedAt, ContractObjId, IsSite
FROM            dbo.vwFlatContractObject
WHERE        (IsSite = 1)
```

It looks like a normal flat object but with site filtered out. We should be doing more than this.

It should return all the instruments added to a site contract.

### 2.1 Finding organization that is aligned with the site contract specification

Use IsSite = 1 to find all contracts. It should give a list of all the site contracts and their account number + GEIS guid. Now, let's go to the Latest Syngio table to link them, of course the table needs to be fixed first.

```sql
SELECT        dbo.vw_Syngoi_incl_co.id AS InstrumentId, dbo.vw_LatestSystemQuery.System, dbo.vw_Syngoi_incl_co.QueryTimeStamp, dbo.vw_Syngoi_incl_co.NetworkName, dbo.vw_Syngoi_incl_co.NetworkId, 
                         dbo.vw_Syngoi_incl_co.LinkedAccount, dbo.vw_Syngoi_incl_co.InstrumentGroup, dbo.vw_Syngoi_incl_co.SerialNumber, dbo.vw_Syngoi_incl_co.InstrumentName, dbo.vw_Syngoi_incl_co.ContractNumber, 
                         dbo.vw_Syngoi_incl_co.ContractId, dbo.vw_Syngoi_incl_co.ShipTo, dbo.vwFlatContractObjectSiteOnly.ContractNumber AS SiteContract, dbo.vwFlatContractObjectSiteOnly.IsSite
FROM            dbo.vw_Syngoi_incl_co LEFT OUTER JOIN
                         dbo.vwFlatContractObjectSiteOnly ON dbo.vw_Syngoi_incl_co.LinkedAccount = dbo.vwFlatContractObjectSiteOnly.GeisID RIGHT OUTER JOIN
                         dbo.vw_LatestSystemQuery ON dbo.vw_Syngoi_incl_co.QueryTimeStamp = dbo.vw_LatestSystemQuery.QueryTimeStamp AND dbo.vw_Syngoi_incl_co.System = dbo.vw_LatestSystemQuery.System
```

After bueatification:

```sql
SELECT
    i.id AS InstrumentId,
    t.System,
    i.QueryTimeStamp,
    i.NetworkName,
    i.NetworkId,
    i.LinkedAccount,
    i.InstrumentGroup,
    i.SerialNumber,
    i.InstrumentName,
    i.ContractNumber,
    i.ContractId,
    i.ShipTo
FROM
    dbo.vw_Syngoi_incl_co i
    RIGHT OUTER JOIN dbo.vw_LatestSystemQuery t ON i.QueryTimeStamp = t.QueryTimeStamp
    AND i.System = t.System
```

The follow up table Latest Syngio is just this + the organization based on ship to. With the new org table, this can be done with one step, skipping the consolidation in separate tables:

```sql
SELECT
    i.id AS InstrumentId,
    t.System,
    i.QueryTimeStamp,
    i.NetworkName,
    i.NetworkId,
    i.LinkedAccount,
    i.InstrumentGroup,
    i.SerialNumber,
    i.InstrumentName,
    i.ContractNumber,
    i.ContractId,
    i.ShipTo,
	o.Name AS OrgName,
	o.AccountNumber AS OrgAccountNo,
	o.GeisGuid AS OrgGEISGuid,
	o.Region,
	o.Country,
	o.City,
	o.Street
FROM
    dbo.vw_Syngoi_incl_co i
    RIGHT OUTER JOIN dbo.vw_LatestSystemQuery t 
    ON 
    	i.QueryTimeStamp = t.QueryTimeStamp
    	AND i.System = t.System
	LEFT OUTER JOIN iqx.Org o 
	ON
		i.LinkedAccount = o.GeisGuid

```

### 2.1 Bind the Site Only contracts to actual Latest Syngio

We shall find the site contract specifications:

```sql
SELECT
    i.[System],
    i.[SerialNumber],
    c.[ContractNumber],
    c.[AccountNumber],
    c.[Name],
    c.[GeisGuid],
    c.[Region]
FROM
    [vw_LatestSysnetinstOrg] i
    LEFT JOIN [vwFlatContractObject] c ON c.GeisGuid = i.orggeisguid
WHERE
    c.IsSite = 1
```

Searching for these instrument numbers in Flat object normal table:

```
SELECT
    
    i.[SerialNumber],
	i.[System],
    c.[ContractNumber],
    c.[AccountNumber],
    c.[Name],
    c.[GeisGuid],
    c.[Region]
FROM
    [vw_LatestSysnetinstOrg] i
    LEFT JOIN [vwFlatContractObject] c ON c.[GeisGuid] = i.[OrgGEISGuid]
WHERE
    c.IsSite = 1
```

From a design point of view, the flat contract object table is OK. It just means what serial number and contracts are bonded to an account.

The fix should be applied to Instrument Link Status.

## 3. Continue with dep repair

Next in line is the Instrument Link Status tables.

```sql
SELECT
    COALESCE (O.SerialNumber, C.SerialNumber) AS SerialNumber,
    C.ContractNumber,
    C.RecordedAt,
    C.Country,
    C.Region,
    CASE
        WHEN C.IsSite = 1 THEN C.PartnerName
        ELSE O.AccountName
    END AS AccountName,
    CASE
        WHEN C.IsSite = 1 THEN C.PartnerId
        ELSE O.AccountNumber
    END AS AccountNumber,
    O.City,
    O.Street,
    O.LinkedAccount,
    TRIM(O.System) AS System,
    O.InstrumentName,
    O.InstrumentGroup,
    O.NetworkName,
    O.QueryTimeStamp,
    C.Description,
    C.InstrumentType,
    C.ContractObjId,
    O.SiteContract,
    O.ShipTo,
    C.IsSite,
    O.NetworkId,
    C.ValidTo
FROM
    dbo.vw_LatestSysnetinstOrg AS O FULL
    OUTER JOIN dbo.vwFlatContractObject AS C ON O.SerialNumber = C.SerialNumber
```



The Link Status is optimized without middle tables now:

```sql
SELECT
	COALESCE (O.SerialNumber, C.SerialNumber) AS SerialNumber,

    C.ContractNumber,
    C.RecordedAt,
    
	C.AccountNumber,
    
    O.LinkedAccount, 

    TRIM(O.System) AS System,
	O.NetworkId,
    O.NetworkName,
    O.InstrumentGroup,
    O.InstrumentName,
    O.QueryTimeStamp,
    C.Description,
    C.InstrumentType,
    C.ContractObjId,

    C.IsSite,

    C.ValidTo,

	DATEDIFF(DAY, GETDATE(), C.ValidTo) AS Valid,

	CASE WHEN C.ContractNumber IS NULL THEN 1 ELSE 0 END | 
	CASE WHEN O.System IS NULL THEN 2 ELSE 0 END |
	CASE WHEN O.NetworkId IS NULL THEN 4 ELSE 0 END |
	CASE WHEN LinkedAccount = 0x0 THEN 8 ELSE 0 END |
	CASE WHEN LinkedAccount IS NULL THEN 16 ELSE 0 END |
	CASE WHEN AccountNumber IS NULL THEN 32 ELSE 0 END
	AS StatusFlag




FROM
    dbo.LatestSyngio AS O 
FULL OUTER JOIN 
	dbo.vwFlatContractObject AS C 
ON 
	O.SerialNumber = C.SerialNumber

ORDER BY RecordedAt DESC
```

