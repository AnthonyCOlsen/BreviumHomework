using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
	public class Scheduler
	{
		private readonly ApiWrapper _apiWrapper;

		public Scheduler() {
			_apiWrapper = new ApiWrapper();
		}

		public async Task RunTest()
		{
			// Call API endpoint each once to confirm it works

			await _apiWrapper.PostStart();

			AppointmentRequest appointmentRequest = await _apiWrapper.GetAppointmentRequest();

			System.Collections.Generic.ICollection<AppointmentInfo> appointmentInfoCollection = (System.Collections.Generic.ICollection<AppointmentInfo>)await _apiWrapper.GetSchedule();

			try
			{
				AppointmentInfoRequest appointmentInfoRequest = new AppointmentInfoRequest()    // This is temporary bogus test data
				{
					RequestId = 1,
					PersonId = 1,
					DoctorId = Doctor._1,
					AppointmentTime = DateTime.Now,	// Wrong date-range and time
					IsNewPatientAppointment = true,
				};
				await _apiWrapper.PostSchedule(appointmentInfoRequest);
			}
			catch (Exception ex)
			{
				// Catch, eat, and ignore the exception here. Yes, we know the data is bogus... it's just a test call. Ignore (for now)
				Utilities.LogErrorException(ex);
			}

			System.Collections.Generic.ICollection<AppointmentInfo> appointmentInfoCollection2 = (System.Collections.Generic.ICollection<AppointmentInfo>)await _apiWrapper.PostStop();
		}
	}
}
