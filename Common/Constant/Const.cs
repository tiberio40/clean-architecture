namespace Common.Constant
{
    public class Const
    {
        public struct TypeClaims
        {
            public const string
                IdUser = "IdUser",
                Email = "Email",
                FullName = "FullName",
                IdRol = "IdRol";
        }

        public struct ClientConst
        {
            public const string
                NetSuite = "NetSuite",
                Tmetric = "Tmetric",
                Meta = "Meta";
        }

        public struct OAuthTypeConst
        {
            public const string
                OAuth1 = "OAuth1.0",
                Bearer = "Bearer";
        }

        public struct ComponentMetaTypeConst {
            public const string
                Header = "HEADER",
                Body = "BODY",
                Footer = "FOOTER";
        }

        public struct MetaStatusConst
        {
            public const string
                Approved = "APPROVED",
                Pending = "PENDING",
                Reject = "REJECTED",
                NoFound = "NOFOUND",
                Draft = "DRAFT";
        }

        public struct MetaTypeConst
        {
            public const string
                WhatsApp = "whatsapp";
        }
    }
}
