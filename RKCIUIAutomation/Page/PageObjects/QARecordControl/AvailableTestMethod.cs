using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    public class AvailableTestMethod
    {
        public enum A_TestMethodType
        {
            [StringValue("AASHTO-T11")] AASHTO_T11,
            [StringValue("AASHTO-T27")] AASHTO_T27,
            [StringValue("DB000")] DB000,
            [StringValue("ASTM-C31")] ASTM_C31,
            [StringValue("ASTM-C39")] ASTM_C39,
            [StringValue("AASHTO-T111")] AASHTO_T111,
            [StringValue("ASTM-C117")] ASTM_C117,
            [StringValue("ASTM-C136")] ASTM_C136,
            [StringValue("ASTM-D6913")] ASTM_D6913,
            [StringValue("T176")] T176,
            [StringValue("Tex-0000")] Tex_0000,
            [StringValue("Tex-203-F")] Tex_203_F,
            [StringValue("Tex-401-A")] Tex_401_A,
            [StringValue("Tex-402-A")] Tex_402_A,
            [StringValue("Tex-406-A")] Tex_406_A,
            [StringValue("Tex-408-A")] Tex_408_A,
            [StringValue("Tex-413-A")] Tex_413_A,
            [StringValue("Tex-418 Truck Log")] Tex_418_TruckLog,
            [StringValue("Tex-418-A")] Tex_418_A,
            [StringValue("Tex-423-A")] Tex_423_A,
            [StringValue("Tex-423II-A")] Tex_423II_A,
            [StringValue("Tex-424-A")] Tex_424_A,
            [StringValue("Tex-436-A")] Tex_436_A,
            [StringValue("Tex-460-A")] Tex_460_A,
            [StringValue("Tex-418DM-A")] Tex_418DM_A
        }

        public enum E_TestMethodType
        {
            [StringValue("AASHTO-T11")] AASHTO_T11,
            [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
            [StringValue("AASHTO-T27")] AASHTO_T27,
            [StringValue("AASHTO-T288")] AASHTO_T288,
            [StringValue("AASHTO-T289")] AASHTO_T289,
            [StringValue("ASTM-C117")] ASTM_C117,
            [StringValue("ASTM-C136")] ASTM_C136,
            [StringValue("ASTM-D2216")] ASTM_D2216,
            [StringValue("ASTM-D4318")] ASTM_D4318,
            [StringValue("ASTM-D6913")] ASTM_D6913,
            [StringValue("CT226")] CT226,
            [StringValue("DB000")] DB000,
            [StringValue("T255/T265")] T255_T265,
            [StringValue("T290")] T290,
            [StringValue("T89/T90")] T89_T90,
            [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
            [StringValue("AASHTO-M145-S")] AASHTO_M145_S,
            [StringValue("ASTM-C136 - Standard Test Method for Sieve Analysis of Fine and Coarse Aggregates (Split Sieve)")] ASTM_C136_SplitSieve,
            [StringValue("CT202")] CT202,
            [StringValue("ASTM-D2419-14")] ASTM_D2419_14,
            [StringValue("CT217")] CT217,
            [StringValue("T176")] T176,
            [StringValue("AASHTO-T99/T180")] AASHTO_T99_T180,
            [StringValue("ASTM-D1557")] ASTM_D1557,
            [StringValue("ASTM-D4718")] ASTM_D4718,
            [StringValue("ASTM-D6951")] ASTM_D6951,
            [StringValue("Tex-0000")] Tex_0000,
            [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
            [StringValue("Tex-103-E")] Tex_103_E,
            [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
            [StringValue("Tex-107-E")] Tex_107_E,
            [StringValue("Tex-110-E")] Tex_110_E,
            [StringValue("Tex-111-E")] Tex_111_E,
            [StringValue("Tex-115-E")] Tex_115_E,
            [StringValue("Tex-116-E")] Tex_116_E,
            [StringValue("Tex-117-E")] Tex_117_E,
            [StringValue("Tex-128-E")] Tex_128_E,
            [StringValue("Tex-129-E")] Tex_129_E,
            [StringValue("Tex-140-E")] Tex_140_E,
            [StringValue("Tex-145-E")] Tex_145_E,
            [StringValue("Tex-148-E")] Tex_148_E,
            [StringValue("Tex-124-E")] Tex_124_E,
            [StringValue("Tex-203-F")] Tex_203_F,
            [StringValue("Tex-113-E")] Tex_113_E,
            [StringValue("Tex-114-E")] Tex_114_E,
            [StringValue("Tex-120-E")] Tex_120_E,
            [StringValue("Tex-121-E")] Tex_121_E,
            [StringValue("AASHTO-T310")] AASHTO_T310

        }

        public enum F_TestMethodType
        {
            [StringValue("AASHTO-T329")] AASHTO_T329,
            [StringValue("DB000")] DB000,
            [StringValue("Tex-0000")] Tex_0000,
            [StringValue("Tex-200/342-F")] Tex_200_342_F,
            [StringValue("Tex-200-F")] Tex_200_F,
            [StringValue("Tex-207-F")] Tex_207_F,
            [StringValue("Tex-227-F")] Tex_227_F,
            [StringValue("Tex-228-F")] Tex_228_F,
            [StringValue("Tex-236-F")] Tex_236_F
        }


        internal static class I15North
        {
            internal enum A1
            {
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("DB000")] DB000
            }

            internal enum E1
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("AASHTO-T310")] AASHTO_T310,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum E2
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum E3
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("AASHTO-T99/T180")] AASHTO_T99_T180,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum F1
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F2
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F3
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }
        }

        internal static class I15South
        {
            internal enum A1
            {
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("DB000")] DB000,
                [StringValue("ASTM-C31")] ASTM_C31,
                [StringValue("ASTM-C39")] ASTM_C39
            }

            internal enum E1
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("AASHTO-T310")] AASHTO_T310,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum E2
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum E3
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T99/T180")] AASHTO_T99_T180,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum F1
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F2
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F3
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }
        }

        internal static class I15Tech
        {
            internal enum A1
            {
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("DB000")] DB000,
                [StringValue("ASTM-C31")] ASTM_C31,
                [StringValue("ASTM-C39")] ASTM_C39
            }

            internal enum E1
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("AASHTO-T310")] AASHTO_T310,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum E2
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum E3
            {
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("AASHTO-T99/T180")] AASHTO_T99_T180,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S
            }

            internal enum F1
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F2
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F3
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }
        }

        internal static class LAX
        {
            internal enum A1
            {
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T111")] AASHTO_T111,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("ASTM-C117")] ASTM_C117,
                [StringValue("ASTM-C136")] ASTM_C136,
                [StringValue("ASTM-D6913")] ASTM_D6913,
                [StringValue("DB000")] DB000,
                [StringValue("T176")] T176,
                [StringValue("ASTM-C31")] ASTM_C31,
                [StringValue("ASTM-C39")] ASTM_C39
            }

            internal enum E1
            {
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("ASTM-C117")] ASTM_C117,
                [StringValue("ASTM-C136")] ASTM_C136,
                [StringValue("ASTM-D2216")] ASTM_D2216,
                [StringValue("ASTM-D4318")] ASTM_D4318,
                [StringValue("ASTM-D6913")] ASTM_D6913,
                [StringValue("CT226")] CT226,
                [StringValue("DB000")] DB000,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S,
                [StringValue("ASTM-C136 - Standard Test Method for Sieve Analysis of Fine and Coarse Aggregates (Split Sieve)")] ASTM_C136_SplitSieve,
                [StringValue("CT202")] CT202
            }

            internal enum E2
            {
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("ASTM-C117")] ASTM_C117,
                [StringValue("ASTM-C136")] ASTM_C136,
                [StringValue("ASTM-D2216")] ASTM_D2216,
                [StringValue("ASTM-D2419-14")] ASTM_D2419_14,
                [StringValue("ASTM-D4318")] ASTM_D4318,
                [StringValue("ASTM-D6913")] ASTM_D6913,
                [StringValue("CT217")] CT217,
                [StringValue("CT226")] CT226,
                [StringValue("DB000")] DB000,
                [StringValue("T176")] T176,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T290")] T290,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S,
                [StringValue("ASTM-C136 - Standard Test Method for Sieve Analysis of Fine and Coarse Aggregates (Split Sieve)")] ASTM_C136_SplitSieve,
                [StringValue("CT202")] CT202
            }

            internal enum E3
            {
                [StringValue("AASHTO-T11")] AASHTO_T11,
                [StringValue("AASHTO-T11/T27")] AASHTO_T11_T27,
                [StringValue("AASHTO-T27")] AASHTO_T27,
                [StringValue("AASHTO-T288")] AASHTO_T288,
                [StringValue("AASHTO-T289")] AASHTO_T289,
                [StringValue("AASHTO-T99/T180")] AASHTO_T99_T180,
                [StringValue("ASTM-C117")] ASTM_C117,
                [StringValue("ASTM-C136")] ASTM_C136,
                [StringValue("ASTM-D1557")] ASTM_D1557,
                [StringValue("ASTM-D2216")] ASTM_D2216,
                [StringValue("ASTM-D2419-14")] ASTM_D2419_14,
                [StringValue("ASTM-D4318")] ASTM_D4318,
                [StringValue("ASTM-D4718")] ASTM_D4718,
                [StringValue("ASTM-D6913")] ASTM_D6913,
                [StringValue("CT217")] CT217,
                [StringValue("CT226")] CT226,
                [StringValue("DB000")] DB000,
                [StringValue("T176")] T176,
                [StringValue("T255/T265")] T255_T265,
                [StringValue("T89/T90")] T89_T90,
                [StringValue("AASHTO-M145-N")] AASHTO_M145_N,
                [StringValue("AASHTO-M145-S")] AASHTO_M145_S,
                [StringValue("ASTM-C136 - Standard Test Method for Sieve Analysis of Fine and Coarse Aggregates (Split Sieve)")] ASTM_C136_SplitSieve,
                [StringValue("CT202")] CT202
            }

            internal enum F1
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F2
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }

            internal enum F3
            {
                [StringValue("AASHTO-T329")] AASHTO_T329,
                [StringValue("DB000")] DB000
            }
        }

        internal static class SH249
        {
            internal enum A1
            {
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-203-F")] Tex_203_F,
                [StringValue("Tex-401-A")] Tex_401_A,
                [StringValue("Tex-402-A")] Tex_402_A,
                [StringValue("Tex-406-A")] Tex_406_A,
                [StringValue("Tex-408-A")] Tex_408_A,
                [StringValue("Tex-413-A")] Tex_413_A,
                [StringValue("Tex-418-A")] Tex_418_A,
                [StringValue("Tex-418DM-A")] Tex_418DM_A,
                [StringValue("Tex-423-A")] Tex_423_A,
                [StringValue("Tex-423II-A")] Tex_423II_A,
                [StringValue("Tex-424-A")] Tex_424_A,
                [StringValue("Tex-436-A")] Tex_436_A,
                [StringValue("Tex-460-A")] Tex_460_A
            }

            internal enum E1
            {
                [StringValue("ASTM-D6951")] ASTM_D6951,
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
                [StringValue("Tex-103-E")] Tex_103_E,
                [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
                [StringValue("Tex-107-E")] Tex_107_E,
                [StringValue("Tex-110-E")] Tex_110_E,
                [StringValue("Tex-111-E")] Tex_111_E,
                [StringValue("Tex-115-E")] Tex_115_E,
                [StringValue("Tex-116-E")] Tex_116_E,
                [StringValue("Tex-117-E")] Tex_117_E,
                [StringValue("Tex-128-E")] Tex_128_E,
                [StringValue("Tex-129-E")] Tex_129_E,
                [StringValue("Tex-140-E")] Tex_140_E,
                [StringValue("Tex-145-E")] Tex_145_E,
                [StringValue("Tex-148-E")] Tex_148_E
            }

            internal enum E2
            {
                [StringValue("ASTM-D6951")] ASTM_D6951,
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
                [StringValue("Tex-103-E")] Tex_103_E,
                [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
                [StringValue("Tex-107-E")] Tex_107_E,
                [StringValue("Tex-110-E")] Tex_110_E,
                [StringValue("Tex-111-E")] Tex_111_E,
                [StringValue("Tex-116-E")] Tex_116_E,
                [StringValue("Tex-117-E")] Tex_117_E,
                [StringValue("Tex-128-E")] Tex_128_E,
                [StringValue("Tex-129-E")] Tex_129_E,
                [StringValue("Tex-145-E")] Tex_145_E,
                [StringValue("Tex-148-E")] Tex_148_E,
                [StringValue("Tex-203-F")] Tex_203_F
            }

            internal enum E3
            {
                [StringValue("ASTM-D6951")] ASTM_D6951,
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
                [StringValue("Tex-103-E")] Tex_103_E,
                [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
                [StringValue("Tex-110-E")] Tex_110_E,
                [StringValue("Tex-111-E")] Tex_111_E,
                [StringValue("Tex-113-E")] Tex_113_E,
                [StringValue("Tex-114-E")] Tex_114_E,
                [StringValue("Tex-117-E")] Tex_117_E,
                [StringValue("Tex-120-E")] Tex_120_E,
                [StringValue("Tex-121-E")] Tex_121_E,
                [StringValue("Tex-129-E")] Tex_129_E,
                [StringValue("Tex-148-E")] Tex_148_E,
                [StringValue("Tex-203-F")] Tex_203_F
            }

            internal enum F1
            {
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-200/342-F")] Tex_200_342_F,
                [StringValue("Tex-200-F")] Tex_200_F,
                [StringValue("Tex-207-F")] Tex_207_F,
                [StringValue("Tex-227-F")] Tex_227_F,
                [StringValue("Tex-228-F")] Tex_228_F,
                [StringValue("Tex-236-F")] Tex_236_F
            }

            internal enum F2
            {
                [StringValue("Tex-0000")] Tex_0000
            }

            internal enum F3
            {
                [StringValue("Tex-0000")] Tex_0000
            }
        }

        internal static class SG
        {
            internal enum A1
            {
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-203-F")] Tex_203_F,
                [StringValue("Tex-401-A")] Tex_401_A,
                [StringValue("Tex-402-A")] Tex_402_A,
                [StringValue("Tex-406-A")] Tex_406_A,
                [StringValue("Tex-408-A")] Tex_408_A,
                [StringValue("Tex-413-A")] Tex_413_A,
                [StringValue("Tex-418 Truck Log")] Tex_418_TruckLog,
                [StringValue("Tex-418-A")] Tex_418_A,
                [StringValue("Tex-423-A")] Tex_423_A,
                [StringValue("Tex-423II-A")] Tex_423II_A,
                [StringValue("Tex-424-A")] Tex_424_A,
                [StringValue("Tex-436-A")] Tex_436_A,
                [StringValue("Tex-460-A")] Tex_460_A
            }

            internal enum E1
            {
                [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-103-E")] Tex_103_E,
                [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
                [StringValue("Tex-107-E")] Tex_107_E,
                [StringValue("Tex-110-E")] Tex_110_E,
                [StringValue("Tex-111-E")] Tex_111_E,
                [StringValue("Tex-115-E")] Tex_115_E,
                [StringValue("Tex-116-E")] Tex_116_E,
                [StringValue("Tex-117-E")] Tex_117_E,
                [StringValue("Tex-124-E")] Tex_124_E,
                [StringValue("Tex-128-E")] Tex_128_E,
                [StringValue("Tex-129-E")] Tex_129_E,
                [StringValue("Tex-140-E")] Tex_140_E,
                [StringValue("Tex-145-E")] Tex_145_E,
                [StringValue("Tex-148-E")] Tex_148_E
            }

            internal enum E2
            {
                [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-103-E")] Tex_103_E,
                [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
                [StringValue("Tex-107-E")] Tex_107_E,
                [StringValue("Tex-110-E")] Tex_110_E,
                [StringValue("Tex-111-E")] Tex_111_E,
                [StringValue("Tex-116-E")] Tex_116_E,
                [StringValue("Tex-117-E")] Tex_117_E,
                [StringValue("Tex-124-E")] Tex_124_E,
                [StringValue("Tex-128-E")] Tex_128_E,
                [StringValue("Tex-129-E")] Tex_129_E,
                [StringValue("Tex-145-E")] Tex_145_E,
                [StringValue("Tex-148-E")] Tex_148_E,
                [StringValue("Tex-203-F")] Tex_203_F
            }

            internal enum E3
            {
                [StringValue("Tex-101-E-Part-III")] Tex_101_E_Part_III,
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-103-E")] Tex_103_E,
                [StringValue("Tex-104/105/106-E")] Tex_104_105_106_E,
                [StringValue("Tex-107-E")] Tex_107_E,
                [StringValue("Tex-110-E")] Tex_110_E,
                [StringValue("Tex-111-E")] Tex_111_E,
                [StringValue("Tex-113-E")] Tex_113_E,
                [StringValue("Tex-114-E")] Tex_114_E,
                [StringValue("Tex-116-E")] Tex_116_E,
                [StringValue("Tex-117-E")] Tex_117_E,
                [StringValue("Tex-120-E")] Tex_120_E,
                [StringValue("Tex-121-E")] Tex_121_E,
                [StringValue("Tex-124-E")] Tex_124_E,
                [StringValue("Tex-148-E")] Tex_148_E,
                [StringValue("Tex-203-F")] Tex_203_F
            }

            internal enum F1
            {
                [StringValue("Tex-0000")] Tex_0000,
                [StringValue("Tex-200/342-F")] Tex_200_342_F,
                [StringValue("Tex-200-F")] Tex_200_F,
                [StringValue("Tex-207-F")] Tex_207_F,
                [StringValue("Tex-227-F")] Tex_227_F,
                [StringValue("Tex-228-F")] Tex_228_F,
                [StringValue("Tex-236-F")] Tex_236_F
            }

            internal enum F2
            {
                [StringValue("Tex-0000")] Tex_0000
            }

            internal enum F3
            {
                [StringValue("Tex-0000")] Tex_0000
            }
        }
    }
}