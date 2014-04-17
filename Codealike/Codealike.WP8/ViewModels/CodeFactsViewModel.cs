namespace Codealike.WP8.ViewModels
{
    using System.Linq;
    using System.Collections.Generic;

    using Models;
    using PortableLogic.Tools;
    using PortableLogic.Communication.ApiModels;

    public class CodeFactsViewModel : ViewModelBase
    {
        private readonly IPageNavigationService _pageNavigationService;
        private UserData _userData;
        private List<UserSkill> _userSkills;
        private List<string> _skillColors;

        public CodeFactsViewModel(IPageNavigationService pageNavigationService)
            : base(pageNavigationService)
        {
            _pageNavigationService = pageNavigationService;
            DisplayName = "Code facts";
            InitializeUI();
        }

        private void InitializeUI()
        {
            _skillColors = new List<string>();
            _skillColors.Add("RoyalBlue");
            _skillColors.Add("YellowGreen");
            _skillColors.Add("Purple");
            _skillColors.Add("NavyBlue");
        }

        private void LoadUserSkills()
        {
            var skills = new List<UserSkill>();
            var otherSkills = new UserSkill();
            otherSkills.Name = "Other";
            for ( int index = 0; index < UserData.ByTechnologies.Count; index++ )
            {
                if ( UserData.ByTechnologies[index].Name == "Other" )
                    otherSkills.Percentage += UserData.ByTechnologies[index].Percentage;
                else
                    skills.Add(GetUserSkillFrom(index));

            }
            skills.Add(otherSkills);
            skills = skills.OrderByDescending(s => s.Percentage).Where(s => s.Percentage > 0).Take(5).ToList();
            UserSkills = skills;
        }

        private UserSkill GetUserSkillFrom(int index)
        {
            string color = string.Empty;
            var technology = UserData.ByTechnologies[index];
            color = GetColor(index, color);
            var userSkill = new UserSkill();
            CreateUserSkill(userSkill, color, technology);
            return userSkill;
        }

        private static void CreateUserSkill(UserSkill userSkill, string color, Technology technology)
        {
            userSkill.Color = color;
            userSkill.Name = technology.Name;
            userSkill.Percentage = technology.Percentage;
            userSkill.Extension = technology.Extension;
            userSkill.Categories = technology.Categories;
            userSkill.Width = (int)( technology.Percentage * 3 );
        }

        private string GetColor(int index, string color)
        {
            if ( index >= _skillColors.Count || index < 0 )
                color = "Gray";
            if ( color == string.Empty )
                color = _skillColors[index];
            return color;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            UserData = _pageNavigationService.Data["UserData"] as UserData;
            LoadUserSkills();
        }

        public List<UserSkill> UserSkills
        {
            get { return _userSkills; }
            set
            {
                if ( Equals(value, _userSkills) )
                    return;
                _userSkills = value;
                NotifyOfPropertyChange(() => UserSkills);
            }
        }

        public UserData UserData
        {
            get { return _userData; }
            set
            {
                if ( Equals(value, _userData) )
                    return;
                _userData = value;
                NotifyOfPropertyChange(() => UserData);
            }
        }
    }
}
