using WarehouseApplication.Data;
using WarehouseApplication.Utilities;

namespace WarehouseApplication
{
    public class Application
    {
        public Warehouse Warehouse { get; private set; }

        public Application()
        {
            Warehouse = new Warehouse();
        }

        public void Start()
        {
            Console.WriteLine("\n       Vitej v evidenci skladu     \n");

            Akce();
        }
        private void Akce()
        {
            Console.WriteLine("\nAkce:");
            Console.WriteLine("1. Zobrazit polozky na sklade");
            Console.WriteLine("2. Pridat polozku");
            Console.WriteLine("3. Vymazat polozku");
            Console.WriteLine("\n0. Ukoncit aplikaci");

            while (true)
            {
                Console.Write("\nVyber akci: ");
                var input = Console.ReadLine();

                try
                {
                    var akce = int.Parse(input);
                    switch(akce)
                    {
                        case 0:
                        {
                            Environment.Exit(0);
                            break;
                        }
                        case 1:
                        {
                            ShowItems();
                            break;
                        }
                        case 2:
                        {
                            AddItem();
                            break;
                        }
                        case 3:
                        {
                            DeleteItem();
                            break;
                        }
                        default:
                            throw new ActionNotFoundException();
                    }
                } 
                catch (ActionNotFoundException)
                {
                    Console.WriteLine("Tato akce neexistuje!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Zadej cislo!");
                }
            }
        }
        
        private void ShowItems()
        {
            var items = Warehouse.Items;
            if (items.Count > 0)
            {
                Console.WriteLine("\nPolozky:");
                Console.WriteLine("Type | Nazev | Mnozstvi");

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    Console.WriteLine($"{i + 1}. {item.Type}, {item.Name}, {item.Amount}");
                }
            } 
            else
            {
                Console.WriteLine("\nNa sklade nejsou zadne polozky!");
            }

            Akce();
        }
         
        private void AddItem()
        {
            while(true)
            {
                var types = Enum.GetValues<ItemType>();
                Console.WriteLine($"\nDostupne typy: {string.Join(", ", types)}");

                Console.WriteLine("\nNapis ve tvaru: typ,nazev,mnozstvi");
                var input = Console.ReadLine();

                var info = input.Split(",");
                if (info.Length != 3) continue;

                if (!Enum.TryParse(info[0], out ItemType itemType))
                {
                    Console.WriteLine("Tento typ polozky neexistuje!");
                    continue;
                }

                if (!int.TryParse(info[2], out int amount))
                {
                    Console.WriteLine("Spatne zadane mnozstvi!");
                    continue;
                }

                var item = new Item(info[1], itemType, amount);
                Console.WriteLine("\nPolozka byla uspesne pridana.\n");
                Warehouse.Items.Add(item); 

                break;
            }

            Akce();
        }

        private void DeleteItem()
        {
            var items = Warehouse.Items;
            if (items.Count > 0)
            {
                Console.WriteLine("\nPolozky:");
                Console.WriteLine("Typ | Nazev | Mnozstvi");

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    Console.WriteLine($"{i + 1}. {item.Type}, {item.Name}, {item.Amount}");
                }

                while(true)
                {
                    Console.WriteLine("\nVyber index ktery chces vymazat: ");
                    var input = Console.ReadLine();

                    if (!int.TryParse(input, out int index))
                    {
                        Console.WriteLine("Tato polozka neexistuje!");
                        continue;
                    }

                    index--;
                    if (index < 0 || index > items.Count)
                    {
                        Console.WriteLine("Tato polozka neexistuje!");
                        continue;

                    }

                    Warehouse.Items.RemoveAt(index);
                    Console.WriteLine("\nPolozka byla uspesne smazana.");
                    break;
                }

            }
            else
            {
                Console.WriteLine("\nNa sklade nejsou zadne polozky!");
            }

            Akce();
        }
    }
}
