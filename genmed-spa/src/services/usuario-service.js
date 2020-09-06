import axios from 'axios'
import authHeader from './auth-header'

class UsuarioService {
  getUsuarios () {
      return axios.get('/usuario', { headers: authHeader() })
  }

  getUsuariosNoAsignado () {
      return axios.get('/usuario/usuarionoasignado', { headers: authHeader() })
  }

  getUsuarioByGuid (guid) {
    return axios.get('/usuario/' + guid, { headers: authHeader() })
  }

  CreateUsuario (usuarioToCreate) {
      var data = JSON.stringify(usuarioToCreate)
      return axios.post('/usuario/registrar', data, { headers: authHeader() })
  }

  UpdateUsuario (usuarioToUpdate) {
    var data = JSON.stringify(usuarioToUpdate)
    return axios.post('/usuario/actualizar', data, {
      headers: authHeader()
    })
  }

  UpdateClave (claveToUpdate) {
    var data = JSON.stringify(claveToUpdate)
    return axios.post('/usuario/actualizarclave', data, {
      headers: authHeader()
    })
  }

  DeactivateUsuario (guid) {
    return axios.post('/usuario/desactivar/' + guid, {
      headers: authHeader()
    })
  }

  ActivateUsuario (guid) {
    return axios.post('/usuario/activar/' + guid, {
      headers: authHeader()
    })
  }
}

export default new UsuarioService()
