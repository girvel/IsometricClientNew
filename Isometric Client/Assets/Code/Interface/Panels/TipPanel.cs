﻿using Assets.Code.Common;
using Assets.Code.Interface.Table;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Interface.Panels
{
    public class TipPanel : Panel<TipPanel>
    {
        private int _currentTipIndex;



        public Text TipIndexText;
        
        public string[] Tips { get; set; }

        public int CurrentTipIndex
        {
            get { return _currentTipIndex; }
            private set
            {
                _currentTipIndex = value;

                Text = Tips[CurrentTipIndex];
                TipIndexText.text = string.Format("{0}/{1}", CurrentTipIndex + 1, Tips.Length);
            }
        }


        protected override void Start()
        {
            base.Start();

            Tips = new[]
            {
                "В этом окне будут появляться подсказки. Нажмите \">\", чтобы " +
                "отобразить следующую подсказку, \"<\", чтобы отобразить предыдущую, " +
                "и \"Остановить обучение\", чтобы навсегда скрыть это окно.",

                "Здания вашей деревни отмечены белыми флагами. Неподалеку располагается " +
                "нейтральное поселение - у него флаги серые.",

                "На данный момент жителям вашей деревни не хватает еды и дров. Для " +
                "начала нужно построить 3 хижины охотников и 2 лесопилки." +
                "\n\nЧтобы начать строить здания, выберите режим управления зданиями или нажмите " +
                "\"1\", после чего выберите клетку, в которой вы хотите построить здание.",

                "Чтобы защищаться от нападений врагов и нападать самому, вам потребуется " +
                "армия. Вы сможете тренировать отряды пехоты, построив казармы пехоты.",

                "Для развития поселения вам потребуется проводить исследования. Для " +
                "этого начните строительство дома духов.",

                "Когда казармы будут готовы, выберите их в режиме управления зданиями и " +
                "закажите одну армию.",

                "Когда дом духов достроится, начните исследование \"Инструменты\" в режиме " +
                "исследований. Это исследование позволит вам изготавливать инструменты, " +
                "ускоряющие добычу ресурсов.",

                "Когда армия будет готова, вы сможете напасть на ближайшее поселение. Разрушая " +
                "постройки противника, вы будете получать ресурсы и людей." +
                "\n\nЧтобы управлять армией, выберите режим управления армией или нажмите кнопку " +
                "\"3\", после чего выберите клетку, в которой располагается ваша армия. Кнопка " +
                "\"Переместить армию\" отдает приказ переместить армию в ту ячейку, которую вы " +
                "выберите следующей, а \"Разрушить здание\" приказывает разграбить и уничтожить " +
                "здание, на котором располагается армия.",
            };

            if (Settings.Current.ShowTips)
            {
                ActionProcessor.Current.AddActionToQueue(() => CurrentTipIndex = 0);
            }
            else
            {
                Stop();
            }
        }


        public void NextTip()
        {
            CurrentTipIndex = (CurrentTipIndex + 1) % Tips.Length;
        }

        public void PreviousTip()
        {
            CurrentTipIndex = (CurrentTipIndex - 1 + Tips.Length) % Tips.Length;
        }

        public void Stop()
        {
            Settings.Current.ShowTips = false;

            Ui.Current.TipForm.SetActive(false);
        }
    }
}