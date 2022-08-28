using System.Collections.Generic;
using WorkManager.DAL.Models;
using WorkManager.DAL.Repositories;
using WorkManager.MySQLsettings;

namespace WorkManager.Responses
{
    public class CreateDefaultClients
    {
        /// <summary>
        /// Список клиентов и компаний
        /// </summary>
        private List<Client> _clients = new List<Client>()
        {
            new Client { Id =1, FirstName = "Veda", LastName = "Richmond", Email = "ligula@necluctus.edu", Company = "Quisque Ac Libero LLP", Age = 42 },
            new Client { Id =2, FirstName = "Demetria", LastName = "Andrews", Email = "feugiat.metus@penatibuset.org", Company = "Nulla Facilisi Foundation", Age = 31 },
            new Client { Id =3, FirstName = "Byron", LastName = "Holmes", Email = "neque.Sed.eget@non.co.uk", Company = "Et Associates", Age = 63 },
            new Client { Id =4, FirstName = "Alexander", LastName = "Cummings", Email = "egestas.ligula@ultricesDuisvolutpat.ca", Company = "Vel Institute", Age = 23 },
            new Client { Id =5, FirstName ="Melinda", LastName="Miles", Email = "justo.nec.ante@nonummyFusce.ca", Company ="Eu NibhVulputate Company", Age =64},
            new Client { Id =6, FirstName ="Dustin", LastName="Beck", Email = "iaculis@afeugiat.edu", Company ="Nec Diam Incorporated",Age =35},
            new Client { Id =7, FirstName ="Ralph", LastName="Maddox", Email = "ipsum@vulputatelacus.co.uk", Company ="Enim Corp.",Age =22},
            new Client { Id =8, FirstName ="Levi", LastName="Zamora", Email = "lectus.a.sollicitudin@nuncQuisque.com", Company ="Sodales At Velit Corp.", Age =57 },
            new Client { Id =9, FirstName ="Driscoll", LastName="Estrada", Email = "Phasellus@Craspellentesque.org", Company ="Id MollisNec LLC", Age =37},
            new Client { Id =10, FirstName ="Hiram", LastName="Mejia", Email = "lacus.Mauris@semper.ca", Company ="Donec TinciduntDonec Industries", Age =59},
            new Client { Id =11, FirstName ="Mason", LastName="Jefferson", Email = "Integer.vitae.nibh@nibh.co.uk", Company ="LectusJusto Ltd", Age =65},
            new Client { Id =12, FirstName ="Nigel", LastName="Rich", Email = "id@faucibusleoin.net", Company ="Tristique Ac Ltd",Age =52},
            new Client { Id =13, FirstName ="Tarik", LastName="Hughes", Email = "enim@ultricesDuisvolutpat.edu", Company ="LacusVarius Et Associates", Age =58},
            new Client { Id =14, FirstName ="Wallace", LastName="Gross", Email = "Curabitur.ut.odio@anteMaecenasmi.co.uk", Company="Rhoncus Id Corporation", Age = 29},
            new Client { Id =15, FirstName ="Arden", LastName="Rivers", Email = "magna.nec.quam@lobortis.net", Company ="VivamusCorporation", Age =59},
            new Client { Id =16, FirstName ="Vincent", LastName="Fox", Email = "faucibus.Morbi.vehicula@ipsumdolor.edu", Company="Imperdiet Dictum Magna Foundation", Age =54},
            new Client { Id =17, FirstName ="Aphrodite", LastName="Randolph", Email = "ac@scelerisquesedsapien.org", Company ="Mattis Foundation",Age =27},
            new Client { Id =18, FirstName ="Alisa", LastName="Riggs", Email = "montes@scelerisque.edu", Company ="Rutrum Non HendreritConsulting", Age =25},
            new Client { Id =19, FirstName ="Jaime", LastName="Lott", Email = "velit.Quisque.varius@aliquetmolestie.net", Company="Ut LLC", Age =61},
            new Client { Id =20, FirstName ="Jamalia", LastName="Buchanan", Email = "arcu.eu.odio@congue.ca", Company ="Curabitur SedTortor Ltd", Age =61},
            new Client { Id =21, FirstName ="Raya", LastName="Mckenzie", Email = "Integer.sem.elit@bibendumsedest.net", Company ="InInc.", Age =43},
            new Client { Id =22, FirstName ="Dante", LastName="Blackwell", Email = "Cras.eget.nisi@Vestibulum.edu", Company ="Nec Foundation",Age =48},
            new Client { Id =23, FirstName ="Kato", LastName="Dickson", Email = "facilisis@doloregestas.co.uk", Company ="Augue ScelerisqueInstitute", Age =60},
            new Client { Id =24, FirstName ="Clio", LastName="Shaffer", Email = "tincidunt@eget.edu", Company ="Dui Augue Eu Limited",Age =29},
            new Client { Id =25, FirstName ="Hamilton", LastName="Kidd", Email = "magna@felisegetvarius.net", Company ="Enim Incorporated",Age =26},
            new Client { Id =26, FirstName ="Kerry", LastName="Oneil", Email = "Suspendisse.eleifend@Crasdolor.com", Company ="InterdumInc.", Age =48},
            new Client { Id =27, FirstName ="Mohammad", LastName="Thompson", Email = "elit.pretium.et@malesuadafamesac.com", Company ="Facilisis Eget Ipsum Inc.", Age = 34},
            new Client { Id =28, FirstName ="Vernon", LastName="Cardenas", Email = "felis@conguea.org", Company ="Iaculis Quis Consulting",Age =35},
            new Client { Id =29, FirstName ="Murphy", LastName="Weaver", Email = "Proin@feugiatnecdiam.org", Company ="Integer UrnaInstitute", Age =63},
            new Client { Id =30, FirstName ="Xena", LastName="Hart", Email = "facilisis.facilisis.magna@loremutaliquam.net", Company="Orci Industries", Age =39 },
            new Client { Id =31, FirstName ="Ivor", LastName="Lara", Email = "Proin.ultrices.Duis@lacuspede.com", Company ="AnteFoundation", Age =30},
            new Client { Id =32, FirstName ="Dana", LastName="Merritt", Email = "et.magnis@Sed.edu", Company ="Eget Industries",Age =53},
            new Client { Id =33, FirstName ="Brielle", LastName="Woodward", Email = "elit.Nulla@magna.edu", Company ="Lorem VehiculaEt Foundation", Age =45},
            new Client { Id =34, FirstName ="Hasad", LastName="Duran", Email = "et@nislsem.co.uk", Company ="Magna Suspendisse Consulting",Age =49},
            new Client { Id =35, FirstName ="Quamar", LastName="Moses", Email = "Proin.sed.turpis@imperdiet.co.uk", Company ="ErosInstitute", Age =32},
            new Client { Id =36, FirstName ="Scarlet", LastName="Barlow", Email = "nisl.sem.consequat@idnunc.co.uk", Company ="AeneanMassa Consulting", Age =58},
            new Client { Id =37, FirstName ="Courtney", LastName="Foley", Email = "urna@mauris.org", Company ="Mauris Associates",Age =47},
            new Client { Id =38, FirstName ="Kennedy", LastName="Shields", Email = "Cras@Nullam.org", Company ="Id Nunc Interdum LLC",Age =40},
            new Client { Id =39, FirstName ="Eve", LastName="Maynard", Email = "metus.sit@lorem.ca", Company ="Pellentesque UltriciesAssociates", Age =30},
            new Client { Id =40, FirstName ="Debra", LastName="Ellis", Email = "Nullam@pretium.ca", Company ="Nulla Tincidunt Industries",Age =24},
            new Client { Id =41, FirstName ="Vivian", LastName="Mcguire", Email = "ornare@at.net", Company ="Id Consulting", Age =48},
            new Client { Id =42, FirstName ="Jason", LastName="Mckinney", Email = "tempor.augue@purusNullam.com", Company ="Netus EtInc.", Age =48},
            new Client { Id =43, FirstName ="Patrick", LastName="Small", Email = "fringilla@Proinsed.co.uk", Company ="Hendrerit Institute",Age =61},
            new Client { Id =44, FirstName ="Drew", LastName="Travis", Email = "scelerisque.scelerisque@velit.org", Company ="PenatibusCorp.", Age =55},
            new Client { Id =45, FirstName ="Burke", LastName="Miller", Email = "Suspendisse@aliquet.net", Company ="Quis Diam PellentesquePC", Age =41},
            new Client { Id =46, FirstName ="Ralph", LastName="Medina", Email = "Class.aptent.taciti@mauris.edu", Company ="LoremIpsum Dolor Corp.", Age =55},
            new Client { Id =47, FirstName ="Alana", LastName="Madden", Email = "at.velit.Cras@aptenttacitisociosqu.net", Company="Euismod Est Arcu Institute", Age =33},
            new Client { Id =48, FirstName ="Salvador", LastName="Cohen", Email = "magna.Duis@Phasellus.org", Company ="Purus PC",Age =37},
            new Client { Id =49, FirstName ="Jenette", LastName="Dejesus", Email = "adipiscing.Mauris.molestie@liberoduinec.ca", Company="Lectus Justo Incorporated", Age =56},
            new Client { Id =50, FirstName ="Ramona", LastName="Gilliam", Email = "massa.Vestibulum@lectuspede.ca", Company ="ImperdietDictum LLP", Age =24},
        };
        
        public void Create()
        {
            ClientsRepository clientsRepository = new ClientsRepository(new MySqlClients());

            foreach (Client client in _clients)
            {
                clientsRepository.Create(client);
            }
        }
    }
}
