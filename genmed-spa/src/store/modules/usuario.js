export const usuario = {
    namespaced: true,
    state: {
        usuarioGuid: ''
    },
    actions: {
        setUsuarioGuid ({ commit }, guid) {
            commit('getUsuarioGuid', guid)
        }
    },
    mutations: {
        getUsuarioGuid (state, guid) {
            state.usuarioGuid = guid
        }
    },
    getters: {
        getGuidToEdit (state) {
            return state.usuarioGuid
        }
    }
}
