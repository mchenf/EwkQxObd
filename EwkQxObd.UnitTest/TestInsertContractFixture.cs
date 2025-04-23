using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EwkQxObd.UnitTest
{
    public class TestInsertContractFixture : IEnumerable
    {
        private const int Mask23 =
            0b_00000000_00111111_11111111_11111111;
        private const int Mask13 =
            0b_00000000_00000000_00011111_11111111;

        private const long contractBase =
            5_000_000;

        private static readonly DateOnly startDate = new (1999, 1, 1);

        public IEnumerator GetEnumerator()
        {

            var rng = RandomNumberGenerator.Create();

            byte[] randBytes = new byte[864];
            rng.GetBytes(randBytes);


            //Contract Number requires 23 bits. Offset from 5000000
            //Date starts at 1999, offset range 17y, requires about 13 bits.
            //1 unit of test case require:
            // 32 + 32 = 64 bits
            //
            //To generate 108 test cases, requires 864 bytes



            long ContractNoOff = 0;
            int VfromOff = 0;
            int VtoOff = 0;

            for (int i = 0; i < 864; i+=8)
            {
                //Gen contract num

                int p1 = randBytes[i];
                p1 = randBytes[i + 1] << 8 | p1;
                p1 = randBytes[i + 2] << 16 | p1;
                p1 = randBytes[i + 3] << 24 | p1;
                ContractNoOff = Mask23 & p1;

                p1 = randBytes[i + 4];
                p1 = randBytes[i + 5] << 8 | p1;
                VfromOff = Mask13 & p1;

                p1 = randBytes[i + 6];
                p1 = randBytes[i + 7] << 8 | p1;
                VtoOff = Mask13 & p1;

                startDate.AddDays(VfromOff);
                startDate.AddDays(VtoOff);

                yield return (
                    contractBase + ContractNoOff,
                    startDate.AddDays(VfromOff),
                    startDate.AddDays(VtoOff)
                );
            }


        }
    }
}
