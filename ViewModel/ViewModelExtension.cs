namespace ViewModel
{
	public static class ViewModelExtensions
	{
		public static AdminViewModel ToAdminViewModel(this ViewModel viewModel)
		{
			AdminViewModel model = new AdminViewModel(viewModel.DialogService, viewModel.Culture);
			model.Model = viewModel.Model;

			model.Bodies = viewModel.Bodies;
			model.Chargers = viewModel.Chargers;
			model.Coolers = viewModel.Coolers;
			model.CPUs = viewModel.CPUs;
			model.HDDs = viewModel.HDDs;
			model.Motherboards = viewModel.Motherboards;
			model.RAMs = viewModel.RAMs;
			model.SSDs = viewModel.SSDs;
			model.Videocards = viewModel.Videocards;

			model.BODYFields = viewModel.BODYFields;
			model.CHARGERFields = viewModel.CHARGERFields;
			model.COOLERFields = viewModel.COOLERFields;
			model.CPUFields = viewModel.CPUFields;
			model.HDDFields = viewModel.HDDFields;
			model.MOTHERBOARDFields = viewModel.MOTHERBOARDFields;
			model.RAMFields = viewModel.RAMFields;
			model.SSDFields = viewModel.SSDFields;
			model.VIDEOCARDFields = viewModel.VIDEOCARDFields;

			model.SelectedBody = viewModel.SelectedBody;
			model.SelectedCharger = viewModel.SelectedCharger;
			model.SelectedCooler = viewModel.SelectedCooler;
			model.SelectedCpu = viewModel.SelectedCpu;
			model.SelectedHdd = viewModel.SelectedHdd;
			model.SelectedMotherboard = viewModel.SelectedMotherboard;
			model.SelectedRam = viewModel.SelectedRam;
			model.SelectedSsd = viewModel.SelectedSsd;
			model.SelectedTab = viewModel.SelectedTab;
			model.SelectedVideocard = viewModel.SelectedVideocard;

			return model;
		}

		public static ViewModel ToViewModel(this AdminViewModel viewModel)
		{
			ViewModel model = new ViewModel(viewModel.DialogService, viewModel.Culture);
			model.Model = viewModel.Model;

			model.Bodies = viewModel.Bodies;
			model.Chargers = viewModel.Chargers;
			model.Coolers = viewModel.Coolers;
			model.CPUs = viewModel.CPUs;
			model.HDDs = viewModel.HDDs;
			model.Motherboards = viewModel.Motherboards;
			model.RAMs = viewModel.RAMs;
			model.SSDs = viewModel.SSDs;
			model.Videocards = viewModel.Videocards;

			model.BODYFields = viewModel.BODYFields;
			model.CHARGERFields = viewModel.CHARGERFields;
			model.COOLERFields = viewModel.COOLERFields;
			model.CPUFields = viewModel.CPUFields;
			model.HDDFields = viewModel.HDDFields;
			model.MOTHERBOARDFields = viewModel.MOTHERBOARDFields;
			model.RAMFields = viewModel.RAMFields;
			model.SSDFields = viewModel.SSDFields;
			model.VIDEOCARDFields = viewModel.VIDEOCARDFields;

			model.SelectedBody = viewModel.SelectedBody;
			model.SelectedCharger = viewModel.SelectedCharger;
			model.SelectedCooler = viewModel.SelectedCooler;
			model.SelectedCpu = viewModel.SelectedCpu;
			model.SelectedHdd = viewModel.SelectedHdd;
			model.SelectedMotherboard = viewModel.SelectedMotherboard;
			model.SelectedRam = viewModel.SelectedRam;
			model.SelectedSsd = viewModel.SelectedSsd;
			model.SelectedTab = viewModel.SelectedTab;
			model.SelectedVideocard = viewModel.SelectedVideocard;

			return model;
		}
	}
}