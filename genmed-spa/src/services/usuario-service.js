import axios from 'axios'
import authHeader from './auth-header'

class UsuarioService {
    getUsuarios() {
        return axios.get('/usuario', { headers: authHeader() });
    }
}

export default new UsuarioService();