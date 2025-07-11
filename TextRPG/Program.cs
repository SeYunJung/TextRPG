using System;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace TextRPG
{
    // 싱글톤으로 구현 => 어디에서든 사용하기 위해서 
    public class Save
    {
        private static Save _instance;
        public static Save Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Save();
                }
                return _instance;
            }
        }

        private string path = "save_file.json";

        public void SaveGames(Warrior warrior)
        {
            string json = JsonSerializer.Serialize(warrior, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, json);
            Console.WriteLine("게임이 저장되었습니다.");
        }

        public Warrior LoadGames()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("저장된 게임이 없습니다.");
                return null;
            }

            else
            {
                string json = File.ReadAllText(path);
                Warrior warrior = JsonSerializer.Deserialize<Warrior>(json);
                if(warrior != null)
                {
                    Console.WriteLine("저장된 게임을 불러옵니다.");
                    return warrior;
                }
                else
                {
                    Console.WriteLine("예기치 못한 에러가 발생했습니다.");
                    return null;
                }
            }
        }
    }

    public class Store
    {
        private static Store _instance;
        bool hasItem;

        public static Store Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new Store();
                return _instance;
            }
        }

        private List<Item> _itemList = new List<Item>()
        {
            new Item("수련자 갑옷", 5, 0, "수련에 도움을 주는 갑옷입니다.", 1000),
            new Item("무쇠갑옷", 9, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1500),
            new Item("스파르타의 갑옷", 15, 0, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500),
            new Item("낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검입니다.", 600),
            new Item("청동 도끼", 0, 5, "어디선가 사용했던거 같은 도끼입니다.", 1500),
            new Item("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000),
            new Item("냉기의 활", 0, 5, "냉기의 요정이 사용했던 활입니다.", 1500),
            new Item("유령의 검", 0, 20, "유령의 기운으로 만들어진 검입니다.", 4000),
            new Item("빛의 투구", 15, 0, "빛의 힘이 깃든 투구입니다", 3500)
        };
        public List<Item> GetItemList()
        {
            return _itemList;
        }

        public Store()
        {
            
        }

        public void PrintItemList(List<Item> inventory, bool isPurchased = false)
        {
            for(int i = 0; i < _itemList.Count; i++)
            {
                hasItem = inventory.Any(x => x.Name == _itemList[i].Name);

                // 공격력 아이템이면
                if (_itemList[i].Attack > 0 && _itemList[i].Defense == 0)
                {
                    // 보유한 아이템은 구매완료 표시 
                    // isPurchased가 true이면 인덱스 붙이기. false이면 인덱스 X
                    if (isPurchased)
                    {
                        if (hasItem)
                            Console.WriteLine($"- {i + 1} {_itemList[i].Name.PadRight(10)}|  공격력 +{_itemList[i].Attack}  |  {_itemList[i].Description.PadRight(10)}|  구매완료");
                        else
                            Console.WriteLine($"- {i + 1} {_itemList[i].Name.PadRight(10)}|  공격력 +{_itemList[i].Attack}  |  {_itemList[i].Description.PadRight(10)}|  {_itemList[i].Price} G");
                    }
                    else
                    {
                        if (hasItem)
                            Console.WriteLine($"-  {_itemList[i].Name.PadRight(10)}|  공격력 +{_itemList[i].Attack}  |  {_itemList[i].Description.PadRight(10)}|  구매완료");
                        else
                            Console.WriteLine($"-  {_itemList[i].Name.PadRight(10)}|  공격력 +{_itemList[i].Attack}  |  {_itemList[i].Description.PadRight(10)}|  {_itemList[i].Price} G");
                    }
                }
                // 방어력 아이템이면 
                else if (_itemList[i].Defense > 0 && _itemList[i].Attack == 0)
                {
                    if (isPurchased)
                    {
                        if (hasItem)
                            Console.WriteLine($"- {i + 1} {_itemList[i].Name.PadRight(10)}|  방어력 +{_itemList[i].Defense}  |  {_itemList[i].Description.PadRight(10)}|  구매완료");
                        else
                            Console.WriteLine($"- {i + 1} {_itemList[i].Name.PadRight(10)}|  방어력 +{_itemList[i].Defense}  |  {_itemList[i].Description.PadRight(10)}|  {_itemList[i].Price} G");
                    }
                    else
                    {
                        if (hasItem)
                            Console.WriteLine($"-  {_itemList[i].Name.PadRight(10)}|  방어력 +{_itemList[i].Defense}  |  {_itemList[i].Description.PadRight(10)}|  구매완료");
                        else
                            Console.WriteLine($"-  {_itemList[i].Name.PadRight(10)}|  방어력 +{_itemList[i].Defense}  |  {_itemList[i].Description.PadRight(10)}|  {_itemList[i].Price} G");
                    }

                }
            }
        }
    }

    public class Item
    {
        // 아이템 정보 
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private float _defense;
        public float Defense
        {
            get { return _defense; }
            set { _defense = value; }
        }
        private float _attack;
        public float Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }
        private string statInfo;
        public string StatInfo
        {
            get { return statInfo; }
            set { statInfo = value; }   
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private bool _isEquipped;
        public bool IsEquipped
        {
            set { _isEquipped = value; }
            get { return _isEquipped; }
        }
        private string _equipMark;
        public string EquipMark
        {
            set {  _equipMark = value; }
            get { return _equipMark; }
        }
        private int _price;
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        StringBuilder sb;

        public Item() { }

        public Item(string itemName, float itemDefense, float itemAttack, string itemDescription, int price)
        {
            _name = itemName;
            _defense = itemDefense;
            _attack = itemAttack;
            _description = itemDescription;
            _price = price;

            _isEquipped = false;

            sb = new StringBuilder();
        }
        
        public Item(string itemName, float itemDefense, float itemAttack, string itemDescription) 
        {
            _name = itemName;
            _defense = itemDefense;
            _attack = itemAttack;
            _description = itemDescription;

            _isEquipped = false;

            sb = new StringBuilder();
        }

        // 테스트
        public Item(string itemName, float itemDefense, float itemAttack, string itemDescription, bool isEquipped)
        {
            _name = itemName;
            _defense = itemDefense;
            _attack = itemAttack;
            _description = itemDescription;

            _isEquipped = isEquipped;
            if (_isEquipped) _equipMark = "[E]";
            else _equipMark = "";

            sb = new StringBuilder();
        }

        public string PrintItemInfo(bool isEquipMode = false)
        {
            // 공격력만 있는 아이템은 공격력만, 방어력만 있는 아이템은 방어력만, 둘다 있는 아이템은 둘다 출력하게 하기 
            if (_attack == 0 && _defense == 0)
            {
                statInfo = "";
            }
            else if(_attack == 0)
            {
                statInfo = $"방어력 +{_defense}";
            }
            else if(_defense == 0)
            {
                statInfo = $"공격력 +{_attack}";
            }

            // 장착중인건 E로 표시 
            return $"{_equipMark}{_name.PadRight(10)}|  {statInfo.PadRight(10)}|  {_description}";
        }
    }

    public class Warrior
    {
        // 전사 정보 
        private int _level;
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _chad;
        public string Chad
        {
            get { return _chad;}
            set { _chad = value; }
        }
        private float _attackPower;
        public float AttackPower
        {
            get { return _attackPower;}
            set { _attackPower = value; }
        }
        private float _defense;
        public float Defense
        {
            get { return _defense;}
            set { _defense = value; }
        }
        private int _health;
        public int Health
        {
            get { return _health;}
            set { _health = value; }
        }
        private int _gold;
        public int Gold
        {
            get { return _gold;}
            set { _gold = value; }
        }
        private List<Item> _inventory;
        public List<Item> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        private List<bool> _isEquippedList;
        public List<bool> IsEquippedList
        {
            get { return  _isEquippedList; }
            set { _isEquippedList = value; }
        }
        private float _bonusAttack;
        public float BonusAttack
        {
            get { return _bonusAttack; }
            set { _bonusAttack = value; }
        }
        private float _bonusDefense;
        public float BonusDefense
        {
            get { return _bonusDefense; }
            set { _bonusDefense = value; }
        }

        // 입력 관련 변수 
        private string input;
        public string Input
        {
            get { return input; }
            set { input = value; }
        }
        private int actNumber;
        public int ActNumber
        {
            get { return actNumber; }
            set {  actNumber = value; } 
        }
        private bool isNumber;
        public bool IsNumber
        {
            get { return isNumber; }
            set { isNumber = value; }
        }

        public Warrior()
        {
            _level = 1;
            _chad = "전사";
            _attackPower = 10;
            _defense = 5;
            _health = 100;
            _gold = 1500;

            _inventory = new List<Item>()
            {
                //new Item("무쇠값옷", 5, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", true),
                //new Item("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", true),
                new Item("무쇠갑옷", 5, 0, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1700),
                new Item("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2000),
                new Item("낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600),
                new Item("냉기의 활", 0, 5, "냉기의 요정이 사용했던 활입니다.", 1500),
                new Item("유령의 검", 0, 20, "유령의 기운으로 만들어진 검입니다.", 4000),
            };

            _isEquippedList = new List<bool>()
            {
                false,
                false,
                false,
                false,
                false
            };
        }

        public void SetActNumber()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            input = Console.ReadLine();
            isNumber = int.TryParse(input, out actNumber);
            //do
            //{
            //    Console.WriteLine("원하시는 행동을 입력해주세요.");
            //    Console.Write(">> ");
            //    input = Console.ReadLine();
            //    isNumber = int.TryParse(input, out actNumber);
            //} while (!isNumber || !(0 <= actNumber && actNumber <= 9)); // actNumber가 숫자가 아니거나 0~9사이 숫자가 아니면 되묻기.
        }

        public void PrintStat()
        {
            // 0만 입력할 수 있음.
            // 다른 숫자, 다른 값을 입력하면 잘못입력했으니 다시 입력하라고 하기.
            while(true)
            {
                Console.WriteLine("\n상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine($"Lv. {_level.ToString("D2")}");
                Console.WriteLine($"Chad ( {_chad} )");

                if (_bonusAttack > 0)
                    Console.WriteLine($"공격력 : {_attackPower} (+{_bonusAttack})");
                else
                    Console.WriteLine($"공격력 : {_attackPower}"); 

                if (_bonusDefense > 0)
                    Console.WriteLine($"방어력 : {_defense} (+{_bonusDefense})");
                else
                    Console.WriteLine($"방어력 : {_defense}"); 

                Console.WriteLine($"체 력 : {_health}");
                Console.WriteLine($"Gold : {_gold} G\n");
                Console.WriteLine("0. 나가기\n");

                SetActNumber();

                Console.WriteLine();

                if (actNumber == 0 && isNumber) break;
                else Console.WriteLine("잘못된 입력입니다.");
            }


        }

        public void UpdateEquipStatus()
        {
            Console.WriteLine("\n인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < _inventory.Count; i++)
            {
                Console.WriteLine($"- {i+1} {_inventory[i].PrintItemInfo()}");
            }

            Console.WriteLine();

            Console.WriteLine("0. 나가기\n");

            SetActNumber();

            if (actNumber == 0 && isNumber)
                OpenInventory();
            else
            {
                // 숫자가 아니거나 범위에서 벗어난 숫자를 입력했다면 
                if (!isNumber || !(0 < actNumber && actNumber < _inventory.Count + 1))
                {
                    Console.WriteLine("잘못된 입력입니다.");

                    // 재귀 호출
                    UpdateEquipStatus();
                }
                else
                {
                    Item selectedItem = _inventory[actNumber - 1];
                    if (selectedItem != null)
                    {
                        // 선택한 아이템이 공격력 아이템이면
                        if(selectedItem.Attack > 0 && selectedItem.Defense == 0)
                        {
                            // 선택한 아이템이 이미 장착된 아이템이면
                            if (selectedItem.IsEquipped)
                            {
                                int unequipIndex = _inventory.Select((value, index) => new { value, index }).Where(x1 => x1.value.Attack > 0 && x1.value.IsEquipped).Select(x2 => x2.index).FirstOrDefault();
                                _inventory[unequipIndex].IsEquipped = false;
                                _inventory[unequipIndex].EquipMark = "";
                                _attackPower -= _inventory[unequipIndex].Attack;
                                _bonusAttack -= _inventory[unequipIndex].Attack;
                                Console.WriteLine($"{_inventory[unequipIndex].Name}이(가) 장착해제 되었습니다.");

                                UpdateEquipStatus();
                            }
                            else
                            {
                                // 이미 공격력 아이템을 장착하고 있다면 (인벤토리에서 공격력이 0보다 큰 아이템이 있으면)
                                bool hasAttackItem = _inventory.Count(item => item.Attack > 0 && item.IsEquipped) >= 1;
                                if (hasAttackItem)
                                {
                                    // 그 아이템을 장착 해제하고, 선택한 공격력 아이템을 장착한다. 
                                    int unequipIndex = _inventory.Select((value, index) => new { value, index }).Where(x1 => x1.value.Attack > 0 && x1.value.IsEquipped).Select(x2 => x2.index).FirstOrDefault();
                                    _inventory[unequipIndex].IsEquipped = false;
                                    _inventory[unequipIndex].EquipMark = "";
                                    _attackPower -= _inventory[unequipIndex].Attack;
                                    _bonusAttack -= _inventory[unequipIndex].Attack;
                                    Console.WriteLine($"{_inventory[unequipIndex].Name}이(가) 장착해제 되었습니다.");

                                    selectedItem.IsEquipped = true;
                                    selectedItem.EquipMark = "[E]";
                                    _attackPower += selectedItem.Attack;
                                    _bonusAttack += selectedItem.Attack;
                                    Console.WriteLine($"{selectedItem.Name}이(가) 장착되었습니다.");

                                    UpdateEquipStatus();
                                }

                                // 공격력 아이템을 장착하고 있지 않다면 
                                else
                                {
                                    // 선택한 공격력 아이템을 장착한다. 
                                    selectedItem.IsEquipped = true;
                                    selectedItem.EquipMark = "[E]";
                                    _attackPower += selectedItem.Attack;
                                    _bonusAttack += selectedItem.Attack;
                                    Console.WriteLine($"{selectedItem.Name}이(가) 장착되었습니다.");
                                    UpdateEquipStatus();
                                }
                            }

                            
                        }

                        // 선택한 아이템이 방어구 아이템이면
                        else if (selectedItem.Defense > 0 && selectedItem.Attack == 0)
                        {
                            if (selectedItem.IsEquipped)
                            {
                                int unequipIndex = _inventory.Select((value, index) => new { value, index }).Where(x1 => x1.value.Defense > 0 && x1.value.IsEquipped).Select(x2 => x2.index).FirstOrDefault();
                                _inventory[unequipIndex].IsEquipped = false;
                                _inventory[unequipIndex].EquipMark = "";
                                _defense -= _inventory[unequipIndex].Defense;
                                _bonusDefense -= _inventory[unequipIndex].Defense;
                                Console.WriteLine($"{_inventory[unequipIndex].Name}이(가) 장착해제 되었습니다.");

                                UpdateEquipStatus();
                            }
                            else
                            {
                                // 이미 방어력 아이템을 장착하고 있다면  (인벤토리에서 방어력이 0보다 큰 아이템이 있으면)
                                bool hasDefenseItem = _inventory.Count(item => item.Defense > 0 && item.IsEquipped) >= 1;
                                if (hasDefenseItem)
                                {
                                    // 그 아이템을 장착 해제하고, 선택한 방어력 아이템을 장착한다. 
                                    int unequipIndex = _inventory.Select((value, index) => new { value, index }).Where(x1 => x1.value.Defense > 0 && x1.value.IsEquipped).Select(x2 => x2.index).FirstOrDefault();
                                    _inventory[unequipIndex].IsEquipped = false;
                                    _inventory[unequipIndex].EquipMark = "";
                                    _defense -= _inventory[unequipIndex].Defense;
                                    _bonusDefense -= _inventory[unequipIndex].Defense;
                                    Console.WriteLine($"{_inventory[unequipIndex].Name}이(가) 장착해제 되었습니다.");

                                    selectedItem.IsEquipped = true;
                                    selectedItem.EquipMark = "[E]";
                                    _defense += selectedItem.Defense;
                                    _bonusDefense += selectedItem.Defense;
                                    Console.WriteLine($"{selectedItem.Name}이(가) 장착되었습니다.");
                                    UpdateEquipStatus();
                                }

                                // 방어력 아이템을 장착하고 있지 않다면
                                else
                                {
                                    // 선택한 방어력 아이템을 장착한다. 
                                    selectedItem.IsEquipped = true;
                                    selectedItem.EquipMark = "[E]";
                                    _defense += selectedItem.Defense;
                                    _bonusDefense += selectedItem.Defense;
                                    Console.WriteLine($"{selectedItem.Name}이(가) 장착되었습니다.");
                                    UpdateEquipStatus();
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("존재하지 않는 아이템입니다.");
                        UpdateEquipStatus();
                    }
                }
            }
        }

        public void OpenInventory()
        {
            Console.WriteLine("\n인벤토리");

            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            Console.WriteLine("[아이템 목록]");
            for(int i = 0; i < _inventory.Count; i++)
            {
                Console.WriteLine($"- {_inventory[i].PrintItemInfo()}");
            }

            Console.WriteLine();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("2. 나가기\n");

            SetActNumber();

            switch (actNumber)
            {
                // 장착 관리 
                case 1:
                    UpdateEquipStatus();
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        public void PurchaseItems()
        {
            Console.WriteLine("\n상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{_gold} G\n");

            Console.WriteLine("[아이템 목록]");
            Store.Instance.PrintItemList(_inventory, true);

            Console.WriteLine("\n0. 나가기\n");

            SetActNumber();

            List<Item> itemList = Store.Instance.GetItemList();

            // 일치하는 아이템을 선택했다면
            // 1 ~ _itemList.Count 까지 숫자를 선택했다면
            if(1 <= actNumber && actNumber <= itemList.Count)
            {
                // 이미 구매한 아이템이라면
                bool isPurchased = _inventory.Any(x => x.Name == itemList[actNumber - 1].Name);
                if (isPurchased)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    PurchaseItems();
                }
                else
                {
                    // 보유 금액이 충분하다면
                    if(_gold >= itemList[actNumber - 1].Price)
                    {
                        // 재화 감소
                        _gold -= itemList[actNumber - 1].Price;

                        // 인벤토리에 아이템 추가
                        _inventory.Add(itemList[actNumber - 1]);

                        // 상점에 구매완료 표시
                        Console.WriteLine("구매를 완료했습니다.");
                        PurchaseItems();
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        PurchaseItems();
                    }
                }
            }
            else
            {
                if(actNumber != 0)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
                else
                {
                    OpenStore();
                }
            }
        }

        public void ShowMyItemList()
        {
            foreach(Item item in _inventory)
            {
                int discountPrice = (int)(item.Price * (1 - 0.85f));
                // 공격력 아이템이면
                if (item.Attack > 0 && item.Defense == 0)
                {
                    Console.WriteLine($"- {item.Name.PadRight(10)}|  공격력 +{item.Attack}  |  {item.Description.PadRight(10)}|  {discountPrice} G");
                }
                // 방어력 아이템이면 
                else if(item.Attack == 0 && item.Defense > 0)
                {
                    Console.WriteLine($"- {item.Name.PadRight(10)}|  방어력 +{item.Defense}  |  {item.Description.PadRight(10)}|  {discountPrice} G");
                }
            }

            Console.WriteLine("\n0. 나가기");
        }

        public void Sell(int itemNumber)
        {
            int discoundPrice = (int)(_inventory[itemNumber - 1].Price * (1 - 0.85f));

            // 판매 금액 받기
            // 인벤토리에서 아이템 없애기 
            if (_inventory[itemNumber - 1] != null)
            {
                Console.WriteLine($"{_inventory[itemNumber - 1].Name}이(가) 판매되었습니다.");

                _gold += discoundPrice;
                _inventory.Remove(_inventory[itemNumber - 1]);
                Sellitem();
            }
            else
            {
                Console.WriteLine("존재하지 않는 아이템입니다.");
                Sellitem();
            }
        }

        public void Sellitem()
        {
            Console.WriteLine("\n상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{_gold} G\n");

            Console.WriteLine("[아이템 목록]");
            ShowMyItemList();

            SetActNumber();

            if(actNumber == 0 && isNumber)
            {
                OpenStore();
            }
            else
            {
                if(1 <= actNumber && actNumber <= _inventory.Count)
                {
                    Sell(actNumber);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Sellitem();
                }
            }
        }

        public void OpenStore(bool isPurchased = false)
        {
            if (!isPurchased)
                Console.WriteLine("\n상점");
            else
                Console.WriteLine("\n상점 - 아이템 구매");

            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{_gold} G\n");

            Console.WriteLine("[아이템 목록]");

            // Q. Warrior가 Store를 열려면 Store객체가 필요함. Store객체를 어떻게 가져오지? Warrior가 Store를 만드는 건 이상할텐데.. 
            // -> Store를 싱글톤으로 구현하면 가져올 수 있다. 
            if (!isPurchased)
                Store.Instance.PrintItemList(_inventory);
            else
                Store.Instance.PrintItemList(_inventory, isPurchased);

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");

            SetActNumber();

            Console.WriteLine();

            switch (actNumber)
            {
                case 1:
                    PurchaseItems();
                    break;
                case 2:
                    Sellitem();
                    break;
                default:
                    break;
            }
        }

        public void Clear(int level)
        {
            Random random = new Random();
            int randomNumber = 0;

            Console.WriteLine("던전 클리어");
            int num = (int)_defense - level;
            randomNumber = random.Next(20 - num, 36 - num);

            // 체력 감소 
            _health -= randomNumber;
            Console.WriteLine($"체력이 {randomNumber}만큼 감소했습니다. ({_health + randomNumber}) -> {_health}");

            // 보상
            _gold += 1000;
            Console.WriteLine($"1000G를 획득했습니다. ({_gold - 1000}) -> ({_gold})");

            // 공격력% ~ (공격력*2)% 추가 보상
            int percentNumber = random.Next((int)_attackPower, (int)_attackPower * 2 + 1);
            int plusGold = (int)(_attackPower * (1 - percentNumber * 0.01f));
            _gold += plusGold;
            Console.WriteLine($"추가 골드로 {plusGold}를 획득했습니다. ({_gold - plusGold}) -> ({_gold})");
        }

        public void Defeat()
        {
            Console.WriteLine("실패");

            // 체력 감소
            _health -= (_health / 2);
            Console.WriteLine($"체력이 절반 감소했습니다. 현재 체력: {_health}");
        }

        public void EnterDungeon(int level)
        {
            Random random = new Random();
            int randomNumber = 0;
            switch (level)
            {
                case 1:
                    if(_defense >= 5)
                        Clear(5);
                    else
                    {
                        randomNumber = random.Next(0, 101);

                        if(randomNumber <= 40)
                            Clear(5);
                        else
                            Defeat();
                    }
                    Dungeon();
                    break;
                case 2:
                    if (_defense >= 11)
                        Clear(11);
                    else
                    {
                        randomNumber = random.Next(0, 101);

                        if (randomNumber <= 40)
                            Clear(11);
                        else
                            Defeat();
                    }
                    Dungeon();
                    break;
                case 3:
                    if(_defense >= 17)
                        Clear(17);
                    else
                    {
                        randomNumber = random.Next(0, 101);

                        if (randomNumber <= 40)
                            Clear(17);
                        else
                            Defeat();
                    }
                    Dungeon();
                    break;
            }
        }

        public void Dungeon()
        {
            Console.WriteLine("\n던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다\n");
            Console.WriteLine("1. 쉬운 던전     | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전   | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기\n");

            SetActNumber();

            switch (actNumber)
            {
                case 0:
                    break;
                case 1:
                    EnterDungeon(1);
                    break;
                case 2:
                    EnterDungeon(2);
                    break;
                case 3:
                    EnterDungeon(3);
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Dungeon();
                    break;
            }
        }

        public void TakeBreak()
        {
            if(_gold >= 500)
            {
                _gold -= 500;
                _health = 100;
                Console.WriteLine("휴식을 완료했습니다.");
                Rest();
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
                Rest();
            }
        }

        public void Rest()
        {
            Console.WriteLine("\n휴식하기");
            Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {_gold} G)\n");
            Console.WriteLine("1. 휴식하기\n0. 나가기\n");

            SetActNumber();

            Console.WriteLine();

            switch (actNumber)
            {
                case 1:
                    TakeBreak();
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Rest();
                    break;
            }
        }

        public int GetUserAction()
        {
            string input = "";
            int actNumber = 0;
            bool isNumber = false;

            do
            {
                input = Console.ReadLine();
                isNumber = int.TryParse(input, out actNumber);
            } while (!isNumber);

            return int.Parse(input);
        }

        public void SaveGame(Warrior warrior)
        {
            Save.Instance.SaveGames(warrior);
        }

        public Warrior LoadGame()
        {
            return Save.Instance.LoadGames();
        }

        public void SelectAction(int actNumber)
        {
            switch (actNumber)
            {
                // 상태보기 
                case 1:
                    PrintStat();
                    break;
                case 2:
                    OpenInventory();
                    break;
                case 3:
                    OpenStore();
                    break;
                case 4:
                    Dungeon();
                    break;
                case 5:
                    Rest();
                    break;
                case 6:
                    SaveGame(this);
                    break;
                case 7:
                    Program.warrior = LoadGame();
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
            }

        }
    }

    public class Program
    {
        public static Warrior warrior = new Warrior();

        static void PrintInfo()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 휴식하기\n6. 게임저장\n7. 게임 불러오기\n8. 게임 종료");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
        }

        static void Main(string[] args)
        {
            string input = "";
            int actNumber = 0;
            bool isNumber = false;


            do
            {
                PrintInfo();
                input = Console.ReadLine();
                isNumber = int.TryParse(input, out actNumber);
                
                if(!isNumber || !(1 <= actNumber && actNumber <= 8))
                {
                    Console.WriteLine("잘못된 입력입니다.\n");
                }

                // 올바른 입력을 했을 때
                else
                {
                    warrior.SelectAction(actNumber);
                }
            } while (true);
        }
    }
}
