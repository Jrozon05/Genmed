<template>
    <div>
        <div class="sk-plane"></div>
        <CRow>
            <CCol sm="5">
            <CCard>
                <CCardHeader>
                    <strong>Editar Doctor </strong>
                </CCardHeader>
                <CCardBody>
                    <CRow>
                        <CCol sm="12">
                            <CInput
                            label="Identificación:"
                            disabled
                            v-model="doctor.guid"
                            />
                        </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Nombre"
                        placeholder="Introduzca el nombre"
                        @input="$v.doctor.nombre.$touch()"
                        v-model="doctor.nombre"
                        invalid-feedback="El nombre es un campo requerido"
                        :is-valid="!$v.doctor.nombre.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Apellido"
                        @input="$v.doctor.apellido.$touch()"
                        v-model="doctor.apellido"
                        invalid-feedback="El apellido es un campo requerido"
                        :is-valid="!$v.doctor.apellido.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Posición"
                        @input="$v.doctor.posicion.$touch()"
                        v-model="doctor.posicion"
                        invalid-feedback="La posición es un campo requerido"
                        :is-valid="!$v.doctor.posicion.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <div class="form-group">
                        <label>Usuario</label>
                        <v-select label="nombreUsuario"
                            :options="usuarios"
                            :value="usuarios.usuarioId"
                            v-model="doctor.usuario"
                            placeholder="Seleccionar el usuario"
                            @input="checkUsuarioValue"
                            :class="{ 'invalid' : !isUsuarioValid && isUsuarioValid != null }">
                        </v-select>
                        <p v-if="!isUsuarioValid && isUsuarioValid != null" :class="{'invalid-label' : !isUsuarioValid}">Debe asignar un usuario</p>
                    </div>
                    </CCol>
                    </CRow>
                </CCardBody>
                <CCardFooter>
                        <CButton type="submit" color="success" @click.prevent="updateDoctor">Salvar</CButton>&nbsp;
                        <CButton type="submit" variant="outline" color="dark" @click.prevent="cancelarForm">Cancelar</CButton>
                        <CButton v-if="Boolean(!isActive)" type="submit" variant="outline" :color="Boolean(isActive) ? 'danger' : 'success'" @click="activarDoctor" class="btnFloat">{{Boolean(isActive) ? 'Desactivar' : 'Activar'}}</CButton>
                        <CButton v-if="Boolean(isActive)" type="submit" variant="outline" :color="Boolean(isActive) ? 'danger' : 'success'" @click="deleteDoctor" class="btnFloat">{{Boolean(isActive) ? 'Desactivar' : 'Activar'}}</CButton>
                </CCardFooter>
            </CCard>
        </CCol>
        <CCol sm="6">
        </CCol>
        </CRow>
    </div>
</template>

<script>
import DoctorService from '../../services/doctor-service'
import UsuarioService from '../../services/usuario-service'
import vSelect from 'vue-select'
import 'vue-select/dist/vue-select.css'
import { required, requiredUnless } from 'vuelidate/lib/validators'

export default {
  name: 'Table',
  data () {
      return {
          usuarios: [],
          doctor: {
              guid: '',
              nombre: '',
              apellido: '',
              posicion: '',
              usuarioId: 0,
              usuario: ''
          },
          isUsuarioValid: null,
          isActive: true
      }
  },
  components: {
    vSelect
    },
  validations: {
      doctor: {
          nombre: { required },
          apellido: { required },
          posicion: { required },
          usuarioId: {
              required: requiredUnless(vm => {
                  return vm.usuarioId.$model === 0
              })
          }
      }
  },
  computed: {
      guidToEdit () {
         return this.$route.params.guid
      }
  },
  created () {
      this.changeDoctorInfo(this.$route.params.guid)
  },
  mounted () {
      this.getUsuarios()
  },
  watch: {
      guidToEdit () {
          this.changeDoctorInfo(this.guidToEdit)
      }
  },
  methods: {
    getUsuarios () {
        UsuarioService.getUsuarios().then(
            response => {
                const data = response.data
                for (const key in data) {
                    const usuario = data[key]
                    usuario.id = key
                    this.usuarios.push(usuario)
                }
            }
        )
    },
    changeDoctorInfo (item) {
        DoctorService.getDoctorByGuid(item).then(
            response => {
                const data = response.data
                const doctor = data
                this.isActive = doctor.activo
                this.doctor.nombre = doctor.nombre
                this.doctor.apellido = doctor.apellido
                this.doctor.posicion = doctor.posicion
                this.doctor.guid = doctor.guid
                this.doctor.usuarioId = doctor.usuario.usuarioId
                this.doctor.usuario = doctor.usuario.nombreUsuario
            }
        )
    },
    updateDoctor () {
        this.$v.$touch()
        if (this.$v.doctor.usuarioId.$model === 0) {
            this.isUsuarioValid = false
        }
        if (this.$v.$invalid) {
            return
        }
        const doctorData = {
            guid: this.doctor.guid,
            nombre: this.doctor.nombre,
            apellido: this.doctor.apellido,
            posicion: this.doctor.posicion,
            usuarioId: this.doctor.usuario.usuarioId
        }

        DoctorService.updateDoctor(doctorData).then(
            response => {
                const data = response.data
                const doctor = data

                if (doctor != null) {
                    this.$alertify.success('Doctor Salvado...')
                } else {
                    this.$alertify.warning('Error en salvar el doctor')
                }
            }
        )
    },
    deleteDoctor () {
        DoctorService.deactivateDoctor(this.guidToEdit).then(
            response => {
                const data = response.data
                this.isActive = data.flag
            }
        )
    },
    activarDoctor () {
        DoctorService.activateDoctor(this.guidToEdit).then(
            response => {
                const data = response.data
                this.isActive = data.flag
            }
        )
    },
    checkUsuarioValue (event) {
            if (event === null) {
                this.isUsuarioValid = false
            } else {
                this.isUsuarioValid = true
            }
        },
    cancelarForm () {
        this.$router.push({ name: 'Doctor' })
    },
    success () {
        this.$alertify.success('success')
    },
    warning () {
        this.$alertify.warning('warning')
    }
  }
}
</script>

<style scoped>
    .btnFloat {
        float: right;
    }

    .vs--disabled .vs__clear, .vs--disabled .vs__dropdown-toggle, .vs--disabled .vs__open-indicator, .vs--disabled .vs__search, .vs--disabled .vs__selected {
        cursor: auto;
        background-color: #d8dbe0;
    }

    .invalid {
        border: 1px solid #e55353;
    }

    .invalid-label {
        width: 100%;
        margin-top: 0.25rem;
        font-size: 80%;
        color: #e55353
    }
</style>
