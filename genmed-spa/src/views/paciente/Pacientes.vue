<template>
    <div>
        <CRow>
            <CCol sm="12">
                <CCard>
            <CCardHeader>
                <strong>Lista de Pacientes</strong>
                <div style="float: right;">
                    <CButton
                            color="success"
                            square
                            size="sm"
                            @click.prevent="updateUsuario(item)"
                        >
                            Crear Paciente
                        </CButton>
                </div>
            </CCardHeader>
            <CCardBody>
            <CDataTable
                :fields="fields"
                :items="usuarios"
                table-filter
                items-per-page-select
                :items-per-page="10"
                hover
                sorter
                pagination
                >
                <template #editarUsuario="{item}">
                    <td class="py-2">
                        <CButton
                            color="primary"
                            variant="outline"
                            square
                            size="sm"
                            @click.prevent="updateUsuario(item)"
                        >
                            Modificar
                        </CButton>
                    </td>
                </template>
                <template #consulta="{item}">
                    <td class="py-2">
                        <CButton
                            color="primary"
                            variant="outline"
                            square
                            size="sm"
                            @click.prevent="updateUsuario(item)"
                        >
                            Consulta
                        </CButton>
                    </td>
                </template>
                </CDataTable>
            </CCardBody>
        </CCard>
            </CCol>
        </CRow>
    </div>
</template>

<script>
import UsuarioService from '../../services/usuario-service'
import DoctorService from '../../services/doctor-service'
import { required, email, minLength, sameAs, requiredUnless } from 'vuelidate/lib/validators'

const fields = [
    { key: 'nombreCompleto', label: 'Paciente', _style: 'width:15%' },
    { key: 'expendiente', _style: 'width:15%' },
    { key: 'cedula', _style: 'width:20%' },
    { key: 'edad' },
    { key: 'ocupacion' },
    { key: 'editarUsuario', label: 'Editar', _style: 'width:1%', sorter: false, filter: false },
    { key: 'consulta', label: 'Consulta', _style: 'width:1%', sorter: false, filter: false }
]

export default {
  name: 'Table',
  data () {
      return {
          usuarios: [],
          doctores: [],
          usuario: {
              nombreUsuario: '',
              email: '',
              clave: '',
              confirmarClave: '',
              doctorId: 0,
              rolId: 0
          },
          message: '',
          alert: false,
          fields,
          isDoctorValid: null,
          isRolValid: null
      }
  },
  validations: {
      usuario: {
          nombreUsuario: { required },
          email: {
              required,
              email
          },
          clave: {
              required,
              minLength: minLength(6)
          },
          confirmarClave: {
              required,
              sameAsClave: sameAs('clave')
          },
          doctorId: {
              required: requiredUnless(vm => {
                  return vm.doctorId.$model === 0
              })
          },
          rolId: {
              required: requiredUnless(vm => {
                  return vm.rolId.$model === 0
              })
          }
      }
  },
  mounted () {
      this.getUsuarios()
      this.getDoctores()
  },
  methods: {
    getUsuarios () {
        UsuarioService.getUsuarios().then(
            response => {
                const data = response.data
                for (const key in data) {
                    const usuario = data[key]
                    usuario.id = key
                    usuario.nombreCompleto = usuario.doctor[0].nombreCompleto
                    usuario.posicion = usuario.doctor[0].posicion
                    usuario.rol = usuario.rol.nombre
                    this.usuarios.push(usuario)
                }
            }
        )
    },
    getDoctores () {
        DoctorService.getDoctores().then(
        response => {
            const data = response.data
            for (const key in data) {
                const usuario = data[key]
                usuario.id = key
                this.doctores.push(usuario)
            }
        }
    )
    },
    CreateUsuario () {
        this.$v.$touch()
        if (this.$v.usuario.doctorId.$model === 0) {
            this.isDoctorValid = false
        }
        if (this.$v.usuario.rolId.$model === 0) {
            this.isRolValid = false
        }
        if (this.$v.$invalid) {
            return
        }
        const usuarioData = {
            nombreUsuario: this.usuario.nombreUsuario,
            email: this.usuario.email,
            clave: this.usuario.clave,
            confirmarClave: this.usuario.confirmarClave,
            doctorId: this.usuario.doctorId,
            rolId: parseInt(this.usuario.rolId)
        }

        UsuarioService.CreateUsuario(usuarioData).then(response => {
            const data = response.data
            if (data.error) {
                this.alert = true
                this.message = data.error
                return this.message
            }

            this.alert = false
            const usuario = data
            usuario.usuarioId = data.usuarioId
            usuario.email = data.email
            usuario.nombreCompleto = usuario.doctor[0].nombreCompleto
            usuario.posicion = usuario.doctor[0].posicion
            usuario.rol = usuario.rol.nombre

            if (usuario != null) {
                this.usuarios.push(usuario)
                this.clear()
            }
        })
    },
    updateUsuario (item) {
        this.$router.push({ name: 'Paciente / Create Paciente' })
    },
    checkDoctorValue (event) {
        if (event.target.value === '0') {
            this.isDoctorValid = false
        } else {
            this.isDoctorValid = true
        }
    },
    checkRolValue (event) {
        if (event.target.value === '0') {
            this.isRolValid = false
        } else {
            this.isRolValid = true
        }
    },
    clear () {
        this.usuario.nombreUsuario = ''
        this.usuario.email = ''
        this.usuario.clave = ''
        this.usuario.confirmarClave = ''
        this.usuario.doctorId = 0
        this.usuario.rolId = 0
    }
  }
}
</script>

<style scoped>
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
