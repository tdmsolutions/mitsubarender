using System;
using System.ComponentModel;
using System.IO;
using MitsubaRender.Settings;
using MitsubaRender.Tools;

namespace MitsubaRender.RenderSettings
{
	static class ReconstructionFilterObjectInstances
	{
		public static ISave[] GetReconstructionFilterDefaultInstances()
		{
			return new ISave[] {
				new ReconstructionFilterGaussianFilter(),
				new ReconstructionFilterMitchellNetravaliFilter(),
				new ReconstructionFilterLanczosSincFilter()
			};
		}
	}
	[Serializable]
	class ReconstructionFilterGaussianFilter : ISave
	{
		public ReconstructionFilterGaussianFilter()
		{
			StandardDeviation = 0.5;
		}

		[DisplayName(@"Standard deviation")]
		[Description("Standard deviation of the Gaussian")]
		public double StandardDeviation
		{
			get;
			set;
		}

		public bool Save(string name)
		{
			if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
				name = GetType().Name;

			if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder))
				return false;

			var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder,
			                            name) + LibraryReconstructionFilters.Extension;
			return FileTools.SaveObject(filePath, this);
		}

		public bool Save()
		{
			return Save(null);
		}
	}
	[Serializable]
	class ReconstructionFilterMitchellNetravaliFilter : ISave
	{
		public ReconstructionFilterMitchellNetravaliFilter()
		{
			BParameter = 0.33333;
			CParameter = 0.33333;
		}

		[DisplayName(@"B parameter")]
		[Description("B parameter from the paper")]
		public double BParameter
		{
			get;
			set;
		}

		[DisplayName(@"C parameter")]
		[Description("C parameter from the paper")]
		public double CParameter
		{
			get;
			set;
		}

		public bool Save(string name)
		{
			if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
				name = GetType().Name;

			if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder))
				return false;

			var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder,
			                            name) + LibraryReconstructionFilters.Extension;
			return FileTools.SaveObject(filePath, this);
		}

		public bool Save()
		{
			return Save(null);
		}

	}
	[Serializable]
	class ReconstructionFilterLanczosSincFilter : ISave
	{
		public ReconstructionFilterLanczosSincFilter()
		{
			NumberOfLobes = 3;
		}

		[DisplayName(@"Number of lobes")]
		[Description("Number of filter lobes")]
		public int NumberOfLobes
		{
			get;
			set;
		}

		public bool Save(string name)
		{
			if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
				name = GetType().Name;

			if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder))
				return false;

			var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder,
			                            name) + LibraryReconstructionFilters.Extension;
			return FileTools.SaveObject(filePath, this);
		}

		public bool Save()
		{
			return Save(null);
		}
	}
}
