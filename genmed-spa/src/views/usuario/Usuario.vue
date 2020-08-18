<template>
    <div>
        <CRow>
            <CCol sm="4">
                <CCard>
                <CCardHeader>
                    <strong>Formulario Usuario </strong>
                </CCardHeader>
                <CCardBody>
                    <CRow>
                        <CCol sm="12">
                            <CAlert color="danger" closeButton :show.sync="alert" class="alert-dismissible" v-if="message">
                                {{ message }}
                            </CAlert>
                        </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Nombre Usuario"
                        placeholder="Introduzca su nombre de usuario"
                        @input="$v.usuario.nombreUsuario.$touch()"
                        v-model="usuario.nombreUsuario"
                        invalid-feedback="El nombre de usuario es un campo requerido"
                        :is-valid="!$v.usuario.nombreUsuario.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Correo Electronico"
                        placeholder="Introduzca su correo electronico"
                        @input="$v.usuario.email.$touch()"
                        v-model="usuario.email"
                        :invalid-feedback="!$v.usuario.email.email ? 'No tiene un formato de correo electronico' : 'El correo electronico es un campo requerido'"
                        :is-valid="!$v.usuario.email.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Contraseña"
                        placeholder="Introduzca su constraseña"
                        type="password"
                        v-model="usuario.clave"
                        @input="$v.usuario.clave.$touch()"
                        :invalid-feedback="!$v.usuario.clave.minLength ? 'La clave debe tener al menos 6 letras.' : 'La clave es un campo requerido'"
                        :is-valid="!$v.usuario.clave.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Confirmar Contraseña"
                        placeholder="Confirmar su constraseña"
                        type="password"
                        v-model="usuario.confirmarClave"
                        @input="$v.usuario.confirmarClave.$touch()"
                        :invalid-feedback="!$v.usuario.confirmarClave.sameAsClave ? 'Las contraseñas deben ser idénticas' : 'la confirmación de contraseña es un campo requerido'"
                        :is-valid="!$v.usuario.confirmarClave.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <div class="form-group">
                            <label for="">Roles</label>
                            <select class="form-control" v-model="usuario.rolId" @change="checkRolValue($event)" :class="{ 'invalid' : !isRolValid && isRolValid != null }">
                                <option value="0" selected>Seleccionar Rol</option>
                                <option value="1">Super Admin</option>
                                <option value="2">Admin</option>
                                <option value="3">Usuario</option>
                                <option value="4">Solo Lectura</option>
                            </select>
                            <p v-if="!isRolValid && isRolValid != null" :class="{'invalid-label' : !isRolValid}">Debe asignar un rol</p>
                        </div>
                    </CCol>
                    </CRow>
                </CCardBody>
                    <CCardFooter>
                        <CButton type="submit" color="primary" @click.prevent="createUsuario">Guardar</CButton>
                </CCardFooter>
                </CCard>
            </CCol>
            <CCol sm="8">
                <CCard>
            <CCardHeader>
                <strong>Lista de Usuarios</strong>
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
                </CDataTable>
            </CCardBody>
        </CCard>
            </CCol>
        </CRow>
    </div>
</template>

<script>
import UsuarioService from '../../services/usuario-service'
import { required, email, minLength, sameAs, requiredUnless } from 'vuelidate/lib/validators'

const fields = [
    { key: 'nombreUsuario', _style: 'width:15%' },
    { key: 'email', _style: 'width:20%' },
    { key: 'guid', label: 'Guid' },
    { key: 'rol', label: 'Rol' },
    { key: 'editarUsuario', label: 'Editar', _style: 'width:1%', sorter: false, filter: false }
]

export default {
  name: 'Table',
  data () {
      return {
          usuarios: [],
          usuario: {
              nombreUsuario: '',
              email: '',
              clave: '',
              confirmarClave: '',
              rolId: 0
          },
          message: '',
          alert: false,
          fields,
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
          rolId: {
              required: requiredUnless(vm => {
                  return vm.rolId.$model === 0
              })
          }
      }
  },
  mounted () {
      this.getUsuarios()
  },
  methods: {
      getUsuarios () {
        UsuarioService.getUsuarios().then(
            response => {
                const data = response.data
                for (const key in data) {
                    const usuario = data[key]
                    usuario.id = key
                    usuario.rol = usuario.rol.nombre
                    this.usuarios.push(usuario)
                }
            }
        )
    },
    createUsuario () {
        this.$v.$touch()
        if (this.$v.usuario.rolId.$model === 0) {
            this.isRolValid = false
        }
        if (this.$v.$invalid) {
            return
        }
        const usuarioData = {
            nombreusuario: this.usuario.nombreUsuario,
            email: this.usuario.email,
            clave: this.usuario.clave,
            confirmarClave: this.usuario.confirmarClave,
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
            usuario.rol = usuario.rol.nombre

            if (usuario != null) {
                this.usuarios.push(usuario)
                this.$v.$reset()
                this.clear()
            }
        })
    },
    updateUsuario (item) {
        this.$store.dispatch('usuario/setUsuarioGuid', item.guid).then(
            () => {
                this.$router.push({ name: 'Usuario / Modificar Usuario', params: { guid: item.guid } })
            }
        )
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
