<template>
    <div>
        <CRow>
            <CCol sm="12">
                <CCard>
            <CCardHeader>
                <strong>Listado de Posiciones</strong>
            </CCardHeader>
            <CCardBody>
            <CDataTable
                :fields="fields"
                :items="posiciones"
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
import PosicionService from '../../services/posicion/posicion-service'

const fields = [
    { key: 'descripcion', label: 'Descripcion' },
    { key: 'nombreCorto', label: 'Nombre Corto' },
    { key: 'activo', label: 'Status' },
    { key: 'editarPosicion', label: 'Editar', sorter: false, filter: false }
]
export default {
    name: 'Posicion',
    data () {
        return {
            fields,
            posiciones: [],
            message: '',
            alert: false
        }
    },
    mounted () {
        this.getPosiciones()
    },
    methods: {
        getPosiciones () {
            PosicionService.getPosiciones().then(
                response => {
                    const data = response.data
                    console.log(data)
                    for (const key in data) {
                        const posicion = data[key]
                        posicion.id = posicion.posicionId
                        this.posiciones.push(posicion)
                    }
                }
            )
        }
    }
}
</script>
