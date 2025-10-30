'use strict';

let IdMaps = {};

IdMaps['ContractNumber'] = 'C41_W130_V133_V134_btadminh_struct.object_id';
IdMaps['ContractDescription'] = 'C41_W130_V133_V134_btadminh_struct.description';
IdMaps['CustomerContact'] = 'C41_W130_V133_V134_btpartnerset_contact_name';
IdMaps['EmployeeResponsible'] = 'C41_W130_V133_V134_btpartnerset_emp_resp_name';

IdMaps['DateStart'] = 'C41_W130_V133_V134_btdatecontractstart_date';
IdMaps['DateEnd'] = 'C41_W130_V133_V134_btdatecontractend_date';

let result = {};

result.ContractNumber = document.getElementById(IdMaps.ContractNumber).innerText;
result.ContractDescription = document.getElementById(IdMaps.ContractDescription).innerText;
result.CustomerContact = document.getElementById(IdMaps.CustomerContact).innerText;
result.EmployeeResponsible = document.getElementById(IdMaps.EmployeeResponsible).innerText;

result.DateStart = document.getElementById(IdMaps.DateStart).innerText;
result.DateEnd = document.getElementById(IdMaps.DateEnd).innerText;

