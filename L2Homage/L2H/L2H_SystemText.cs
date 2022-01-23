using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace L2Homage
{
    public class L2H_System_Text
    {
        public virtual string ID { get; }
        public virtual string Text { get; set; }
        public L2H_System_Text()
        {

        }
    }
    public class L2H_Chatfilter : L2H_System_Text
    {
        public Client_Obscene client_Obscene { get; set; }
        
        public L2H_Chatfilter(Client_Obscene client_Obscene)
        {
            this.client_Obscene = client_Obscene;
        }

        public override string ID
        {
            get
            {
                return client_Obscene.ID;
            }
        }
        public override string Text
        {
            get
            {
                return client_Obscene.text;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "CHAT FILTER", "Text", client_Obscene.text, value);
                client_Obscene.text = value;

            }
        }
    }

    public class L2H_EULA
    {
        public Client_Eula client_Eula { get; set; }

        public L2H_EULA(Client_Eula client_Eula)
        {
            this.client_Eula = client_Eula;
        }

        public string Text
        {
            get
            {
                return client_Eula.eula;
            }
            set
            {
                client_Eula.eula = value;
            }

        }
    }

    public class L2H_Gametip : L2H_System_Text
    {
        public Client_Gametip client_Gametip { get; set; }
        public L2H_Gametip(Client_Gametip client_Gametip)
        {
            this.client_Gametip = client_Gametip;
        }
        public override string ID
        {
            get
            {
                return client_Gametip.ID;
            }
        }
        public override string Text
        {
            get
            {
                return client_Gametip.tip;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "GAMETIP", "Text", client_Gametip.tip, value);
                client_Gametip.tip = value;
            }
        }
        public string INT1
        {
            get
            {
                return client_Gametip.int1;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "GAMETIP", "INT1", client_Gametip.int1, value);
                client_Gametip.int1 = value;
            }
        }
        public string INT2
        {
            get
            {
                return client_Gametip.int2;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "GAMETIP", "INT2", client_Gametip.int2, value);
                client_Gametip.int2 = value;
            }
        }
        public bool IsEnabled
        {
            get
            {
                if (client_Gametip.enable_ == "1")
                    return true;
                else
                    return false;
            }
            set
            {

                if (value)
                {
                    L2H_Log.Instance.Log_System_Text_Change(this, "GAMETIP", "Enabled", client_Gametip.enable_, "1");
                    client_Gametip.enable_ = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_System_Text_Change(this, "GAMETIP", "Enabled", client_Gametip.enable_, "0");
                    client_Gametip.enable_ = "0";
                }

            }
        }
        public bool IsUnicode
        {
            get
            {
                return client_Gametip.u_string;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "GAMETIP", "Unicode", client_Gametip.u_string.ToString(), value.ToString());
                client_Gametip.u_string = value;
            }
        }


    }

    public class L2H_NPC_String : L2H_System_Text
    {
        Client_Npc_String client_Npc_String { get; set; }
        public L2H_NPC_String(Client_Npc_String client_Npc_String)
        {
            this.client_Npc_String = client_Npc_String;
        }
        public override string ID
        {
            get
            {
                return client_Npc_String.ID;
            }
        }
        public override string Text
        {
            get
            {
                return client_Npc_String.text;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "NPC STRING", "Text", client_Npc_String.text, value);
                client_Npc_String.text = value;
            }
        }
        public bool IsUnicode
        {
            get
            {
                return client_Npc_String.u_string;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "NPC STRING", "Unicode", client_Npc_String.u_string.ToString(), value.ToString());
                client_Npc_String.u_string = value;
            }
        }
    }

    public class L2H_System_Message : L2H_System_Text, INotifyPropertyChanged
    {
        /*
        
                public string ID;
                public string UNK_0;        // Not sure, but it's always 1
                public string message;
                public string group;        // 0 - none - messages without source (or class if you prefer that term)
                                            // 0 - siege - castle/fortress siege related messages
                                            // 1 - battle - nondamage combat messages(successfull defense, critical hit, miss, etc.)
                                            // 2 - server - general server messages
                                            // 3 - damage - didn't need explanation (you make damage, you take damage)
                                            // 4 - popup - message is displayed in popup window
                                            // 5 - error - if any action produces error, here you go(cancel trade, failed invitation, failed login, etc.)
                                            // 6 - petition - use of petition system
                                            // 7 - useitems - didn't need explanation (eg. use shots, potions, enabled shots, equip armor, etc.)
                public string rgba_0;       // red hex value
                public string rgba_1;       // green hex value
                public string rgba_2;       // blue hex value
                public string rgba_3;       // Opacity
                public string item_sound;   // sound played when message is displayed
                public string sys_msg_ref;  // Source of message
                public string UNK_1_0;      // Position on screen. 1 for top left, 2 for top mid, 3 for top right, 4 for mid left, 5 for mid, 6 for mid right, 7 for bot mid, 8 for bot right, 0 for inside system ui box
                public string UNK_1_1;      // Not sure, but it's always 0
                public string UNK_1_2;      // duration of message in seconds.
                public string UNK_1_3;      // transition speed 0 - quick // 1 - slow // 11 - very slow
                public string UNK_1_4;      // Add decorative effect to message, 1 for true, 0 for false
                public string sub_msg;      // 
                public string type;         //none - messages without source (or class if you prefer that term)
                                            //siege - castle/fortress siege related messages
                                            //battle - nondamage combat messages(successfull defense, critical hit, miss, etc.)
                                            //server - general server messages
                                            //damage - didn't need explanation (you make damage, you take damage)
                                            //popup - message is displayed in popup window
                                            //error - if any action produces error, here you go(cancel trade, failed invitation, failed login, etc.)
                                            //petition - use of petition system
                                            //useitems - didn't need explanation (eg. use shots, potions, enabled shots, equip armor, etc.)

         */
        public Client_System_Message client_System_Message { get; set; }
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public System.Collections.ObjectModel.ObservableCollection<Xceed.Wpf.Toolkit.ColorItem> ColorList
        {
            get
            {
                return L2H_Constants.GetColorList();
            }
        }
        public SolidColorBrush SelectedColorAsBrush
        {
            get
            {
                return new SolidColorBrush(SelectedColor);
            }
        }
        public Color SelectedColor
        {
            get
            {
                string temp_rgba_0 = rgba_0;
                string temp_rgba_1 = rgba_1;
                string temp_rgba_2 = rgba_2;
                string temp_rgba_3 = rgba_3;

                if (rgba_0 == "0")
                    temp_rgba_0 = "00";
                if (rgba_1 == "0")
                    temp_rgba_1 = "00";
                if (rgba_2 == "0")
                    temp_rgba_2 = "00";
                if (rgba_3 == "0")
                    temp_rgba_3 = "00";
                if (rgba_0 == "F")
                    temp_rgba_0 = "FF";

                string colorString = "#" + temp_rgba_3 + temp_rgba_0 + temp_rgba_1 + temp_rgba_2;
                return (Color)ColorConverter.ConvertFromString(colorString);
            }
            set
            {

                rgba_3 = value.ToString()[1] + "" + value.ToString()[2];
                rgba_0 = value.ToString()[3] + "" + value.ToString()[4];
                rgba_1 = value.ToString()[5] + "" + value.ToString()[6];
                rgba_2 = value.ToString()[7] + "" + value.ToString()[8];

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedColorAsBrush"));
            }
        }
        public L2H_System_Message(Client_System_Message client_System_Message)
        {
            this.client_System_Message = client_System_Message;
        }
        public override string ID
        {
            get
            {
                return client_System_Message.ID;
            }
        }
        public override string Text
        {
            get
            {
                return client_System_Message.message;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Text", client_System_Message.message, value);
                client_System_Message.message = value;
            }
        }

        public string UNK_0
        {
            get
            {
                return client_System_Message.UNK_0;
            }
            set
            {
                client_System_Message.UNK_0 = value;
            }
        }
        public string Type //Group number 0 to 7, Type is text
        {
            /*
             // 0 - none - messages without source (or class if you prefer that term)
             // 0 - siege - castle/fortress siege related messages
             // 1 - battle - nondamage combat messages(successfull defense, critical hit, miss, etc.)
             // 2 - server - general server messages
             // 3 - damage - didn't need explanation (you make damage, you take damage)
             // 4 - popup - message is displayed in popup window
             // 5 - error - if any action produces error, here you go(cancel trade, failed invitation, failed login, etc.)
             // 6 - petition - use of petition system
             // 7 - useitems - didn't need explanation (eg. use shots, potions, enabled shots, equip armor, etc.)
             */
            get
            {
                return client_System_Message.group;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Type", client_System_Message.group, value);
                client_System_Message.group = value;
            }
        }
        public string rgba_0
        {
            get
            {
                return client_System_Message.rgba_0;
            }
            set
            {
                client_System_Message.rgba_0 = value;
            }
        }
        public string rgba_1
        {
            get
            {
                return client_System_Message.rgba_1;
            }
            set
            {
                client_System_Message.rgba_1 = value;
            }
        }
        public string rgba_2
        {
            get
            {
                return client_System_Message.rgba_2;
            }
            set
            {
                client_System_Message.rgba_2 = value;
            }
        }
        public string rgba_3
        {
            get
            {
                return client_System_Message.rgba_3;
            }
            set
            {
                client_System_Message.rgba_3 = value;
            }
        }
        public string Sound
        {
            get
            {
                return client_System_Message.item_sound;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Sound", client_System_Message.item_sound, value.ToString());
                client_System_Message.item_sound = value;
            }
        }
        public string sys_msg_ref
        {
            get
            {
                return client_System_Message.sys_msg_ref;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "System Message Ref", client_System_Message.sys_msg_ref, value.ToString());
                client_System_Message.sys_msg_ref = value;
            }
        }
        public int Screen_Position
        {
            get
            {
                return int.Parse(client_System_Message.UNK_1_0);
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Screen Position", client_System_Message.UNK_1_0, value.ToString());
                client_System_Message.UNK_1_0 = value.ToString();
            }
        }
        public string UNK_1_0
        {
            get
            {
                return client_System_Message.UNK_1_0;
            }
            set
            {
                client_System_Message.UNK_1_0 = value;
            }
        }
        public string UNK_1_1
        {
            get
            {
                return client_System_Message.UNK_1_1;
            }
            set
            {
                client_System_Message.UNK_1_1 = value;
            }
        }
        public string Duration
        {
            get
            {
                return client_System_Message.UNK_1_2;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Transition Speed", client_System_Message.UNK_1_2, value);
                client_System_Message.UNK_1_2 = value;
            }
        }
        public string UNK_1_3
        {
            get
            {
                return client_System_Message.UNK_1_3;
            }
            set
            {
                client_System_Message.UNK_1_3 = value;
            }
        }
        public bool Decorative
        {
            get
            {
                if (UNK_1_4 == "1")
                    return true;
                else
                    return false;
            }
            set
            {
                if (value)
                {
                    L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Decorative", client_System_Message.UNK_1_4, "1");
                    UNK_1_4 = "1";
                }
                else
                {
                    L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Decorative", client_System_Message.UNK_1_4, "0");
                    UNK_1_4 = "0";
                }
            }
        }
        public string UNK_1_4
        {
            get
            {
                return client_System_Message.UNK_1_4;
            }
            set
            {
                client_System_Message.UNK_1_4 = value;
            }
        }
        public string Sub_Message
        {
            get
            {
                return client_System_Message.sub_msg;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Sub Message", client_System_Message.sub_msg, value);
                client_System_Message.sub_msg = value;
            }
        }
        public int TypeIndex
        {
            get
            {
                return int.Parse(client_System_Message.group);
            }
            set
            {
                string oldValue = client_System_Message.type;
                bool typeIsSiege = false;
                if (value > 0)
                {
                    value = value - 1;
                    if (value == 0)
                    {
                        typeIsSiege = true;
                        client_System_Message.type = "siege";
                    }
                }

                client_System_Message.group = value.ToString();

                switch (value)
                {
                    case 0:
                        if (!typeIsSiege)
                            client_System_Message.type = "none";
                        break;
                    case 1:
                        client_System_Message.type = "battle";
                        break;
                    case 2:
                        client_System_Message.type = "server";
                        break;
                    case 3:
                        client_System_Message.type = "damage";
                        break;
                    case 4:
                        client_System_Message.type = "popup";
                        break;
                    case 5:
                        client_System_Message.type = "error";
                        break;
                    case 6:
                        client_System_Message.type = "petition";
                        break;
                    case 7:
                        client_System_Message.type = "useitems";
                        break;
                    default:
                        break;
                }

                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Type", oldValue, client_System_Message.type);
            }
        }
        public string type
        {
            get
            {
                return client_System_Message.type;
            }
            set
            {
                client_System_Message.type = value;
            }
        }

        public bool IsUnicode
        {
            get
            {
                return client_System_Message.u_string;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM MESSAGE", "Unicode", client_System_Message.u_string.ToString(), value.ToString());
                client_System_Message.u_string = value;
            }
        }

    }

    public class L2H_System_String : L2H_System_Text
    {
        public Client_System_String client_System_String { get; set; }
        public L2H_System_String(Client_System_String client_System_String)
        {
            this.client_System_String = client_System_String;
        }

        public override string ID
        {
            get
            {
                return client_System_String.ID;
            }
        }
        public override string Text
        {
            get
            {
                return client_System_String.text;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM STRING", "Text", client_System_String.text, value.ToString());
                client_System_String.text = value;
            }
        }
        public bool IsUnicode
        {
            get
            {
                return client_System_String.u_string;
            }
            set
            {
                L2H_Log.Instance.Log_System_Text_Change(this, "SYSTEM STRING", "Unicode", client_System_String.u_string.ToString(), value.ToString());
                client_System_String.u_string = value;
            }
        }
    }

}