import axios from 'axios'
import authHeader from './auth-header'

class DoctorService {
  getDoctores () {
    return axios.get('/doctor', {
      headers: authHeader()
    })
  }

  createDoctor (doctorData) {
    const data = JSON.stringify(doctorData)
    return axios.post('/doctor/registrar', data, {
      headers: authHeader()
    })
  }
}

export default new DoctorService()
