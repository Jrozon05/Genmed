import AuthService from '../services/auth-service'

const usuario = JSON.parse(localStorage.getItem('usuario'))
const initState = usuario ? { status: { loggedIn: true }, usuario } : { status: { loggedIn: false }, usuario: null }

export const auth = {
    namespaced: true,
    state: initState,
    actions: {
        login ({ commit }, usuario) {
            return AuthService.login(usuario).then(
                usuario => {
                    commit('loginSuccess', usuario)
                    return Promise.resolve(usuario)
                },
                error => {
                    console.log(error)
                    commit('loginFailure')
                    return Promise.reject(error)
                }
            )
        }
    },
    mutations: {
        loginSuccess (state, usuario) {
            state.status.loggedIn = true
            state.usuario = usuario
        },
        loginFailure (state) {
            state.status.loggedIn = false
            state.usuario = null
        }
    }
}
