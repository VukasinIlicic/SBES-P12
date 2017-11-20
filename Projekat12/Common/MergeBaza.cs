using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entiteti;

namespace Common
{
    public class MergeBaza
    {
        private static readonly Object lockObject = new Object();
        public List<string> obrisani = new List<string>();  // prvi koji prodje kroz foreach obrise, sledeci kad naidje prvi if ce ga pustiti da doda (zato ovde pamtimo izbrisane pa pre dodavanja proverimo da nisu ovde )

        public MergeBaza()
        {

        }

        public void Merge(Dictionary<string, DataObj> lokalnaBazaServera, Dictionary<string, DataObj> glavnaBaza, int merge)
        {
            foreach (var lbs in lokalnaBazaServera)
            {
                List<int> indeksiAzuriranja = new List<int>();
                List<bool> azurirani = (lbs.Value as DataObj).Azuriran;

                lock(lockObject)
                {
                    if (!glavnaBaza.ContainsKey(lbs.Key) && lbs.Value.Obrisan == false) // dodavanje
                    {
                        if(!obrisani.Contains(lbs.Key))
                        {
                            try
                            {
                                if(merge == Konstanta.MERGE_SA_GLAVNIN || (merge == Konstanta.MERGE_SA_LOKALNIM && lbs.Value.DodatUTajmu))
                                {
                                    lbs.Value.DodatUTajmu = false;
                                    glavnaBaza.Add(lbs.Key, lbs.Value);
                                }
                                    
                            }
                            catch { }
                        }   
                    }
                    else if (glavnaBaza.ContainsKey(lbs.Key) && lbs.Value.Obrisan == true)  // brisanje
                    {
                        if(merge == Konstanta.MERGE_SA_LOKALNIM)
                        {
                            glavnaBaza[lbs.Key].Obrisan = true;
                            continue;
                        }

                        try
                        {
                            glavnaBaza.Remove(lbs.Key);
                            obrisani.Add(lbs.Key);
                        }
                        catch { }   
                    }
                    else if (glavnaBaza.ContainsKey(lbs.Key) && lbs.Value.Obrisan == false && ProveraAzuriranja(indeksiAzuriranja, azurirani))  // azuriranje
                    {
                        for (int i = 0; i < indeksiAzuriranja.Count; i++)
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
