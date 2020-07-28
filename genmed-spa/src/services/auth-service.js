import axios from 'axios'

class AuthService {
  login (usuario) {
    return axios.post('/usuario/login', {
      nombreUsuario: usuario.nombreUsuario,
      clave: usuario.clave
    }).then(response => {
      if (response.data.token) {
        localStorage.setItem('usuario', JSON.stringify(response.data))
      }

      return response.data
    })
  }

  logout () {
    localStorage.removeItem('usuario')
  }
}

export default new AuthService()
