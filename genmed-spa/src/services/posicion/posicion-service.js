import axios from 'axios'
import authHeader from '../auth-header'

class PosicionService {
    getPosiciones () {
        return axios.get('/posicion', { headers: authHeader() })
    }

    createPosicion (posicionToCreate) {
        var data = JSON.stringify(posicionToCreate)
        return axios.post('/posicion', data, { headers: authHeader() })
    }
}

export default new PosicionService()
