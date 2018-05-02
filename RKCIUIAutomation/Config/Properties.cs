using System;
using System.Reflection;

namespace RKCIUIAutomation.Config
{
    public enum TestEnv
    {
        [StringValue("_Test")] Test,
        [StringValue("_Stage")] Stage,
        [StringValue("_PreProd")] PreProd,
        [StringValue("_Prod")] Prod
    }

    public enum Project
    {
        [StringValue("Garnet")] Garnet,
        [StringValue("Green_Line_Extension")] GreenLineExtension,
        [StringValue("I15_Southbound")] I15Southbound,
        [StringValue("I15_Tech_Corridor")] I15TechCorridor,
        [StringValue("SH249")] SH249,
        [StringValue("SMF202")] SMF202,
        [StringValue("Southern_Gateway")] SouthernGateway,
        [StringValue("Tappan_Zee")] TappanZee,
    }

    public enum SiteUrl
    {
        [StringValue("https://elvis3.rkci.com/TZ/Login.aspx")] Tappan_Zee_Prod,
        [StringValue("https://elvis3.rkci.com/TzDev/Login.aspx")] Tappan_Zee_Stage,
        [StringValue("https://elvis3.rkci.com/SR202/Login.aspx")] SMF202_Prod,
        [StringValue("https://elvis3.rkci.com/SR202Staging/Login.aspx")] SMF202_Stage,
        [StringValue("https://garnet.elvispmc.com/")] Garnet_Prod,
        [StringValue("http://stage.garnet.elvispmc.com/")] Garnet_Stage,
        [StringValue("http://test.garnet.elvispmc.com/")] Garnet_Test,
        [StringValue("https://sh249.elvispmc.com/")] SH249_Prod,
        [StringValue("http://stage.sh249.elvispmc.com/")] SH249_Stage,
        [StringValue("http://test.sh249.elvispmc.com/")] SH249_Test,
        [StringValue("https://sg.elvispmc.com/")] Southern_Gateway_Prod,
        [StringValue("http://stage.sg.elvispmc.com/")] Southern_Gateway_Stage,
        [StringValue("http://test.sg.elvispmc.com/")] Southern_Gateway_Test,
        [StringValue("https://i15tech.elvispmc.com/")] I15_Tech_Corridor_Prod,
        [StringValue("http://stage.i15tech.elvispmc.com/")] I15_Tech_Corridor_Stage,
        [StringValue("http://test.i15tech.elvispmc.com/")] I15_Tech_Corridor_Test,
        [StringValue("https://glx.elvispmc.com/")] Green_Line_Extension_Prod,
        [StringValue("http://stage.glx.elvispmc.com/")] Green_Line_Extension_Stage,
        [StringValue("http://test.glx.elvispmc.com/")] Green_Line_Extension_Test,
        [StringValue("https://i15south.elvispmc.com/")] I15_Southbound_Prod,
        [StringValue("http://stage.i15south.elvispmc.com/")] I15_Southbound_Stage,
        [StringValue("http://test.i15south.elvispmc.com/")] I15_Southbound_Test
    }

    public enum TestPlatform
    {
        Win10,
        Mac,
        Android,
        IOS,
        Local
    }

    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge,
        Safari
    }

    internal class StringValueAttribute : Attribute
    {
        public StringValueAttribute(String value)
        {
            Value = value;
        }
        public String Value { get; }
    }


    public static class EnumHelper
    {
        public static string GetString(this Enum value)
        {
            String output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
            output = attrs[0].Value;

            return output;
        }
    }
}
