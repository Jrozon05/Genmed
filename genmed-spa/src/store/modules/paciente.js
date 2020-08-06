export const paciente = {
    namespaced: true,
    state: {
        paciente: {}
    },
    actions: {
        setRegistroGenerales ({ commit }, paciente) {
            commit('registroGenerales', paciente)
        },
        setMotivoConsulta ({ commit }, paciente) {
            commit('motivoConsulta', paciente)
        }
    },
    mutations: {
        registroGenerales (state, paciente) {
            state.paciente = Object.assign(state.paciente, paciente)
        },
        motivoConsulta (state, paciente) {
            state.paciente = Object.assign(state.paciente, paciente)
        }
    }
}
