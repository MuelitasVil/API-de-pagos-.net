namespace API.PaymentTransactions.Shared
{
    public class Enums
    {
        public enum documentTypes
        {
            CC,
            TI,
            CE,
            P
        }

        public enum PaymentFranchise
        {
            PSE,
            VISA,
            MASTERCARD,
            AMERICAN_EXPRESS,
            DINERS_CLUB,
            CODENSA,
            BALOTO,
            EFECTY,
            NEQUI,
            RAPPI_PAY,
            PAYPAL,
            PAYU,
            BANCOLOMBIA_APP,
            DAVIPLATA,
            TPAGA
        }

        public enum IssuerNames
        {
            BANCOLOMBIA,
            BANCO_DE_OCCIDENTE,
            BANCO_POPULAR,
            BANCO_AV_VILLAS,
            BANCO_DE_BOGOTA,
            BANCO_AGRARIO,
            BANCO_DE_COMERCIO_EXTERIOR,
            BANCO_ITAU,
            BANCO_PICHINCHA,
            BANCO_FALABELLA,
            BANCO_CAJA_SOCIAL,
            BANCO_GNB_SUDAMERIS,
            BANCO_PROVINCIAL,
            BANCO_CORPBANCA,
            BANCO_DAVIVIENDA,
            BANCO_BBVA,
            BANCO_SANTANDER,
            BANCO_AGRICOLA,
            BANCO_AGRICOLA_DE_LA_REPUBLICA_DE_COLOMBIA,
            BANCO_AMERICAN_EXPRESS,
            BANCO_AMERICANO,
            BANCO_ATLANTICO,
            BANCO_BOGOTA,
            BANCO_CANADIENSE,
            BANCO_CENTRAL_HISPANO,
            BANCO_CENTRAL_INMOBILIARIO,
            BANCO_COLOMBIANO,
            BANCO_COLOMBIANO_HISPANO,
            BANCO_COLPATRIA,
            BANCO_COLVILLE,
            BANCO_COMMERCIAL,
            BANCO_COMULTRASAN,
            BANCO_CONFEDERADO,
            BANCO_CREDITO_COMERCIAL,
            BANCO_CREDITO_CONSTRUCTOR,
            BANCO_DE_ANTIOQUIA,
            BANCO_DE_CALDAS,
            BANCO_DE_COLOMBIA,
            BANCO_DE_COLOMBIA_SA,
            BANCO_DE_LA_AMISTAD,
            BANCO_DE_LA_CIUDAD,
            BANCO_DE_LA_CONSTITUCION,
            BANCO_DE_LA_GENTE,
            BANCO_DE_LA_INDUSTRIA,
            BANCO_DE_LA_PAZ,
            BANCO_DE_LA_REPUBLICA,
            BANCO_DE_LE_NACION,
            BANCO_DE_LOS_TRABAJADORES,
            BANCO_DE_LONDRES,
            BANCO_DE_SANTANDER,
            BANCO_DE_USTO,
            BANCO_DE_VALENCIA,
            BANCO_DE_VENEZUELA,
            BANCO_DEL_COMERCIO,
            BANCO_DEL_ESTADO,
            BANCO_DEL_HUILA,
            BANCO_DEL_PARQUE,
            BANCO_DEL_PUEBLO,
            BANCO_DEL_QUINDIO,
            BANCO_DEL_TRABAJO,
            BANCO_DEL_VALLE,
            BANCO_DIVERSIFICADO,
            BANCO_FIDEICOMISO,
            BANCO_GANADERO,
            BANCO_GNB,
            BANCO_GUAYAQUIL,
            BANCO_INMOBILIARIO,
            BANCO_INMOBILIARIO_Y_CREDITO,
            BANCO_INVERSION,
            BANCO_LA_PREVISORA,
            BANCO_LE_MARCHAND,
            BANCO_LUIS_ANGEL_ARANGO,
            BANCO_MERCANTIL,
            BANCO_NACIONAL,
            BANCO_NACIONAL_DE_LA_REPUBLICA,
            BANCO_PANAMERICANO,
            BANCO_PARIS,
            BANCO_PASTO,
            BANCO_POPULAR_DEL_VALLE,
            BANCO_RAPUBLICA,
            BANCO_REPUBLICA,
            BANCO_SOCIEDAD_COMERCIAL_COLOMBIANA,
            BANCO_SUDAMERIS,
            BANCO_SUDAMERIS_COLOMBIA,
            BANCO_TEQUENDAMA,
            BANCO_UNION,
            BANCO_VILLAVICENCIO,
            BANCRECER,
            BANEX,
            BANGEN
        }

        public enum PaymentMethods
        {
            PSE,
            CreditCard,
            DebitCard
        }

        public enum currencys 
        {
            COP,
            EUR,
            USD
        }

        public enum PosibleStatus
        {
            APPROVED,
            REFUSED
        }

    }
}
