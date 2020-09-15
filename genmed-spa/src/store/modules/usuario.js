export const usuario = {
    namespaced: true,
    state: {
        usuarioGuid: ''
    },
    actions: {
        setUsuarioGuid ({ commit }, guid) {
            commit('getUsuarioByGuid', guid)
        }
    },
    mutations: {
        getUsuarioByGuid (state, guid) {
            state.usuarioGuid = guid
        }
    },
    getters: {
        getGuidToEdit (state) {
            return state.usuarioGuid
        }
    }
}
