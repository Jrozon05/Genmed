import axios from 'axios'
import authHeader from '../auth-header'

class DireccionService {
    getProvincias () {
        return axios.get('/direccion/provincias', {
            headers: authHeader()
        })
    }

    getCiudades (provinciaId) {
        return axios.get('/direccion/ciudades/' + provinciaId, {
            headers: authHeader()
        })
    }

    getSectores () {
        return axios.get('/direccion/sectores', {
            headers: authHeader()
        })
    }
}

export default new DireccionService()
