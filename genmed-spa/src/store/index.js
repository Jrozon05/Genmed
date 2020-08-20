import Vue from 'vue'
import Vuex from 'vuex'

import { auth } from './modules/auth'
import { sidebar } from './modules/sidebar'
import { usuario } from './modules/usuario'
import { doctor } from './modules/doctor'
import { paciente } from './modules/paciente'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    auth,
    sidebar,
    usuario,
    doctor,
    paciente
  }
})
