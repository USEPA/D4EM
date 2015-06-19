using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D4EMSystemTesting
{
    public class testNatureServe
    {
        public bool testingNatureServe(string aProjectFolder)
        {
            bool pass = false;
            string aProjectFolderNatureServe = System.IO.Path.Combine(aProjectFolder, "NatureServe");
            string aCacheFolder = System.IO.Path.Combine(aProjectFolderNatureServe, "Cache");
            List<string> pollinators = new List<string>();

            pollinators.Add("Anna's Hummingbird (Calypte anna)");
            pollinators.Add("Eastern Tiger Swallowtail (Papilio glaucus)");
            pollinators.Add("Hermit Sphinx (Lintneria eremitus)");
            pollinators.Add("Rusty-patched Bumble Bee (Bombus affinis)");
            pollinators.Add("Southeastern Blueberry Bee (Habropoda laboriosa)");

            bool pollinator1 = false;
            bool pollinator2 = false;
            bool pollinator3 = false;
            bool pollinator4 = false;
            bool pollinator5 = false;

            D4EM.Data.LayerSpecification pollinator_layer = new D4EM.Data.LayerSpecification();
            foreach (string pollinator in pollinators)
            {
                string _pollinator = pollinator.ToString();                
                switch (_pollinator)
                {
                    case "Anna's Hummingbird (Calypte anna)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Calypte_anna;
                        pollinator1 = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);
                        break;
                    case "Eastern Tiger Swallowtail (Papilio glaucus)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Papilio_glaucus;
                        pollinator2 = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);
                        break;
                    case "Hermit Sphinx (Lintneria eremitus)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Lintneria_eremitus;
                        pollinator3 = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);
                        break;
                    case "Rusty-patched Bumble Bee (Bombus affinis)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Bombus_affinis;
                        pollinator4 = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);
                        break;
                    case "Southeastern Blueberry Bee (Habropoda laboriosa)":
                        pollinator_layer = D4EM.Data.Source.NatureServe.LayerSpecifications.Habropoda_laboriosa;
                        pollinator5 = D4EM.Data.Source.NatureServe.getData(aProjectFolderNatureServe, aCacheFolder, pollinator_layer);
                        break;
                }
            }

            if ((pollinator1 == true) && (pollinator2 == true) && (pollinator3 == true) && (pollinator4 == true) && (pollinator5 == true))
            {
                pass = true;
            }
            else
            {
                pass = false;
            }
            return pass;
        }
    }
}
