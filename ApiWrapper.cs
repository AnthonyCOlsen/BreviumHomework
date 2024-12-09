using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class ApiWrapper
    {
        private const string _baseUrl = "https://scheduling.interviews.brevium.com/";
        private readonly Guid _authenticationToken = new Guid("1afc183b-5208-4196-9766-c76095a8d432");
        private HttpClient _httpClient;
        private GeneratedCode _generateCode = null;

        public ApiWrapper()
        {
            _httpClient = new HttpClient();
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationToken.ToString("D"));
            _generateCode = new GeneratedCode(_baseUrl, _httpClient);
        }

        public async Task PostStart()
        {
            try
            {
                Utilities.DisplayMessage("About to post to /api/Scheduling/Start...");
                await _generateCode.StartAsync(_authenticationToken);
                Utilities.DisplayMessage("/api/Scheduling/Start post successful.");
            }
            catch (Exception exception)
            {
                Utilities.DisplayMessage("/api/Scheduling/Start post FAILED!");
                Utilities.LogErrorException(exception);
                throw;
            }
        }

        public async Task<System.Collections.Generic.ICollection<AppointmentInfo>> PostStop()
        {
            try
            {
                Utilities.DisplayMessage("About to post to /api/Scheduling/Stop...");
                System.Collections.Generic.ICollection<AppointmentInfo> appointmentInfoCollection = (System.Collections.Generic.ICollection<AppointmentInfo>)await _generateCode.StopAsync(_authenticationToken);
                Utilities.DisplayMessage("/api/Scheduling/Stop post successful.");
                DisplayAppointmentInfoCollection(appointmentInfoCollection);
                return appointmentInfoCollection;

			}
            catch (Exception exception)
            {
                Utilities.DisplayMessage("/api/Scheduling/Stop post FAILED!");
                Utilities.LogErrorException(exception);
				throw;
			}
		}

        public async Task<AppointmentRequest> GetAppointmentRequest()
        {
            try
            {
                Utilities.DisplayMessage("About to get from /api/Scheduling/AppointmentRequest...");
                AppointmentRequest appointmentRequest = await _generateCode.AppointmentRequestAsync(_authenticationToken);
                Utilities.DisplayMessage("/api/Scheduling/AppointmentRequest get successful.");
                Utilities.DisplayMessage(
                    "AppointmentRequest: " + 
                    $"RequestId: {appointmentRequest.RequestId}, " +
                    $"PersonId: {appointmentRequest.PersonId}, " +
                    $"PreferredDays Count: {appointmentRequest.PreferredDays.Count}, " +
                    $"PreferredDocs Count: {appointmentRequest.PreferredDocs.Count}, " +
                    $"IsNew: {appointmentRequest.IsNew}"
                    );
                return appointmentRequest;
			}
            catch (Exception exception)
            {
                Utilities.DisplayMessage("/api/Scheduling/AppointmentRequest get FAILED!");
                Utilities.LogErrorException(exception);
				throw;
			}
		}

        public async Task<System.Collections.Generic.ICollection<AppointmentInfo>> GetSchedule()
        {
            try
            {
                Utilities.DisplayMessage("About to get from /api/Scheduling/Schedule...");
                System.Collections.Generic.ICollection<AppointmentInfo> appointmentInfoCollection = (System.Collections.Generic.ICollection<AppointmentInfo>)await _generateCode.ScheduleAllAsync(_authenticationToken);
                Utilities.DisplayMessage("/api/Scheduling/Schedule get successful.");
                DisplayAppointmentInfoCollection(appointmentInfoCollection);
                return appointmentInfoCollection;

			}
            catch (Exception exception)
            {
                Utilities.DisplayMessage("/api/Scheduling/Schedule get FAILED!");
                Utilities.LogErrorException(exception);
				throw;
			}
		}

		public async Task PostSchedule(AppointmentInfoRequest appointmentInfoRequest)
		{
			try
			{
                Utilities.RequireParameter("appointmentInfoRequest", appointmentInfoRequest);
				Utilities.DisplayMessage(
					"AppointmentInfoRequest: " +
					$"DoctorId: {appointmentInfoRequest.DoctorId}, " +
					$"PersonId: {appointmentInfoRequest.PersonId}, " +
					$"AppointmentTime: {appointmentInfoRequest.AppointmentTime}, " +
					$"IsNewPatientAppointment: {appointmentInfoRequest.IsNewPatientAppointment}, " +
					$"RequestId: {appointmentInfoRequest.RequestId}"
					);
				Utilities.DisplayMessage("About to post to /api/Scheduling/Schedule...");
				await _generateCode.ScheduleAsync(_authenticationToken, appointmentInfoRequest);
				Utilities.DisplayMessage("/api/Scheduling/Schedule post successful.");
			}
			catch (Exception exception)
			{
				Utilities.DisplayMessage("/api/Scheduling/Schedule post FAILED!");
				Utilities.LogErrorException(exception);
				throw;
			}
		}

		private void DisplayAppointmentInfoCollection(System.Collections.Generic.ICollection<AppointmentInfo> appointmentInfoCollection)
        {
            Utilities.DisplayMessage($"{appointmentInfoCollection.Count} AppointmentInfo records: ");
            foreach (AppointmentInfo appointmentInfo in appointmentInfoCollection)
            {
                Utilities.DisplayMessage(
                    "* " +
                    $"Doctor ID: {appointmentInfo.DoctorId}, " +
                    $"Patient ID: {appointmentInfo.PersonId}, " +
                    $"Time: {appointmentInfo.AppointmentTime}, " +
                    $"New Patient: {appointmentInfo.IsNewPatientAppointment}"
                    );
            }
        }
    }
}
