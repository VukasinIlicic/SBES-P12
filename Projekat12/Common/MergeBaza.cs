using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class MergeBaza
    {
        private static readonly Object lockObject = new Object();

        public MergeBaza()
        {

        }

        public void Merge(Dictionary<string, DataObj> lokalnaBazaServera, Dictionary<string, DataObj> glavnaBaza)
        {
            foreach (var lbs in lokalnaBazaServera)
            {
                List<int> indeksiAzuriranja = new List<int>();
                List<bool> azurirani = (lbs.Value as DataObj).Azuriran;
                if (!glavnaBaza.ContainsKey(lbs.Key) && lbs.Value.Obrisan == false) // dodavanje
                {
                    lock (lockObject)
                    {
                        try
                        {
                            glavnaBaza.Add(lbs.Key, lbs.Value);
                        }
                        catch { }
                    }
                }
                else if (glavnaBaza.ContainsKey(lbs.Key) && lbs.Value.Obrisan == true)  // brisanje
                {
                    lock (lockObject)
                    {
                        try
                        {
                            glavnaBaza.Remove(lbs.Key);
                        }
                        catch { }
                    }
                }
                else if (glavnaBaza.ContainsKey(lbs.Key) && lbs.Value.Obrisan == false && ProveraAzuriranja(indeksiAzuriranja, azurirani))  // azuriranje
                {
                    for (int i = 0; i < indeksiAzuriranja.Count; i++)
                    {
                        lock (lockObject)
                        {
                            try
                            {
                                glavnaBaza[lbs.Key].Potrosnja[indeksiAzuriranja[i]] = (lbs.Value as DataObj).Potrosnja[indeksiAzuriranja[i]];
                            }
                            catch { }
                        }
                    }

                }
            }

        }

        private bool ProveraAzuriranja(List<int> indeksiAzuriranja, List<bool> azurirani)
        {
            bool postojiBaremJedanAzurirani = false;

            for (int i = 0; i < 12; i++)
                if (azurirani[i] == true)
                {
                    indeksiAzuriranja.Add(i);
                    postojiBaremJedanAzurirani = true;
                }

            return postojiBaremJedanAzurirani;
        }

    }
}
