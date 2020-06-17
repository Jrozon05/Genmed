import axios from 'axios'

class AuthService {
  login (usuario) {
    return axios.post('/usuario/login', {
      nombreUsuario: usuario.nombreUsuario,
      clave: usuario.clave
    }).then(response => {
      console.log(response)
      if (response.data.token) {
        localStorage.setItem('usuario', JSON.stringify(response.data))
      }

      return response.data
    })
  }
}

export default new AuthService()
