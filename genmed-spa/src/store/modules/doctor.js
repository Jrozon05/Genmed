export const doctor = {
    namespaced: true,
    state: {
        doctorGuid: ''
    },
    actions: {
        getDoctorGuid ({ commit }, guid) {
            commit('setDoctorGuid', guid)
        }
    },
    mutations: {
        setDoctorGuid (state, guid) {
            state.doctorGuid = guid
        }
    }
}
