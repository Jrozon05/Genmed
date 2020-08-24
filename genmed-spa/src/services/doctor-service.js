import axios from 'axios'
import authHeader from './auth-header'

class DoctorService {
  getDoctores () {
    return axios.get('/doctor', {
      headers: authHeader()
    })
  }

  getDoctorByGuid (guid) {
    return axios.get('/doctor/' + guid, { headers: authHeader() })
  }

  createDoctor (doctorData) {
    const data = JSON.stringify(doctorData)
    return axios.post('/doctor/registrar', data, {
      headers: authHeader()
    })
  }

  updateDoctor (doctorToUpdate) {
    var data = JSON.stringify(doctorToUpdate)
    return axios.post('/doctor/actualizar', data, { headers: authHeader() })
  }

  deactivateDoctor (guid) {
    return axios.post('/doctor/desactivar/' + guid, {
      headers: authHeader()
    })
  }

  activateDoctor (guid) {
    return axios.post('/doctor/activar/' + guid, {
      headers: authHeader()
    })
  }
}

export default new DoctorService()
