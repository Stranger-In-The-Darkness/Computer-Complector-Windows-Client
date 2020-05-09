namespace ViewModel
{
	public static class ViewModelExtensions
	{
		public static AdminViewModel ToAdminViewModel(this ViewModel viewModel)
		{
			AdminViewModel model = new AdminViewModel(viewModel.DialogService, viewModel.Culture)
			{
				Model = viewModel.Model,

				Bodies = viewModel.Bodies,
				Chargers = viewModel.Chargers,
				Coolers = viewModel.Coolers,
				CPUs = viewModel.CPUs,
				HDDs = viewModel.HDDs,
				Motherboards = viewModel.Motherboards,
				RAMs = viewModel.RAMs,
				SSDs = viewModel.SSDs,
				Videocards = viewModel.Videocards,

				BODYFields = viewModel.BODYFields,
				CHARGERFields = viewModel.CHARGERFields,
				COOLERFields = viewModel.COOLERFields,
				CPUFields = viewModel.CPUFields,
				HDDFields = viewModel.HDDFields,
				MOTHERBOARDFields = viewModel.MOTHERBOARDFields,
				RAMFields = viewModel.RAMFields,
				SSDFields = viewModel.SSDFields,
				VIDEOCARDFields = viewModel.VIDEOCARDFields,

				SelectedBody = viewModel.SelectedBody,
				SelectedCharger = viewModel.SelectedCharger,
				SelectedCooler = viewModel.SelectedCooler,
				SelectedCpu = viewModel.SelectedCpu,
				SelectedHdd = viewModel.SelectedHdd,
				SelectedMotherboard = viewModel.SelectedMotherboard,
				SelectedRam = viewModel.SelectedRam,
				SelectedSsd = viewModel.SelectedSsd,
				SelectedTab = viewModel.SelectedTab,
				SelectedVideocard = viewModel.SelectedVideocard
			};

			return model;
		}

		public static ViewModel ToViewModel(this AdminViewModel viewModel)
		{
			ViewModel model = new ViewModel(viewModel.DialogService, viewModel.Culture)
			{
				Model = viewModel.Model,

				Bodies = viewModel.Bodies,
				Chargers = viewModel.Chargers,
				Coolers = viewModel.Coolers,
				CPUs = viewModel.CPUs,
				HDDs = viewModel.HDDs,
				Motherboards = viewModel.Motherboards,
				RAMs = viewModel.RAMs,
				SSDs = viewModel.SSDs,
				Videocards = viewModel.Videocards,

				BODYFields = viewModel.BODYFields,
				CHARGERFields = viewModel.CHARGERFields,
				COOLERFields = viewModel.COOLERFields,
				CPUFields = viewModel.CPUFields,
				HDDFields = viewModel.HDDFields,
				MOTHERBOARDFields = viewModel.MOTHERBOARDFields,
				RAMFields = viewModel.RAMFields,
				SSDFields = viewModel.SSDFields,
				VIDEOCARDFields = viewModel.VIDEOCARDFields,

				SelectedBody = viewModel.SelectedBody,
				SelectedCharger = viewModel.SelectedCharger,
				SelectedCooler = viewModel.SelectedCooler,
				SelectedCpu = viewModel.SelectedCpu,
				SelectedHdd = viewModel.SelectedHdd,
				SelectedMotherboard = viewModel.SelectedMotherboard,
				SelectedRam = viewModel.SelectedRam,
				SelectedSsd = viewModel.SelectedSsd,
				SelectedTab = viewModel.SelectedTab,
				SelectedVideocard = viewModel.SelectedVideocard
			};

			return model;
		}
	}
}