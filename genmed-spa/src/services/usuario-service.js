import axios from 'axios'
import authHeader from './auth-header'

class UsuarioService {
  getUsuarios () {
      return axios.get('/usuario', { headers: authHeader() })
  }

  getUsuarioByGuid (guid) {
    return axios.get('/usuario/' + guid, { headers: authHeader() })
  }

  CreateUsuario (usuarioToCreate) {
      var data = JSON.stringify(usuarioToCreate)
      return axios.post('/usuario/registrar', data, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
  }

  UpdateUsuario (usuarioToUpdate) {
    var data = JSON.stringify(usuarioToUpdate)
    return axios.put('/usuario/update', data, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
  }
}

export default new UsuarioService()
