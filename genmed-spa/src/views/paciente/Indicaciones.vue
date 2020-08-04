<template>
    <div>
        <CAlert color="dark">
            <strong>Indicaciones & Medicamentos</strong>
        </CAlert>
        <CRow>
            <CCol sm="4">
                <CRow>
                    <CCol sm="12">
                        <CAlert color="info">
                            <strong>Crear Indicación</strong>
                        </CAlert>
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Buscar Medicamento"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="6">
                        <CInput
                        label="Nombre del Medicamento"
                        placeholder=""
                        />
                    </CCol>
                    <CCol sm="6">
                        <CInput
                        label="Descripción"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
            <CRow>
                <CCol sm="6">
                    <CInput
                    label="Compuestos del Medicamento"
                    placeholder=""
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Dosis del Medicamento"
                    placeholder=""
                    />
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <CButton type="submit" color="primary" @click.prevent="CreateUsuario">Insertar</CButton>
                </CCol>
            </CRow>
            </CCol>
            <CCol sm="8">
                <CRow>
                    <CCol sm="12">
                        <CAlert color="info">
                            <strong>Listado Indicaciones / Medicamentos</strong>
                        </CAlert>
                    </CCol>
                </CRow>
                <CDataTable
                        :fields="fields"
                        :items="items"
                        hover
                        border
                        >
                        <template #borrarDiagnostico="{item}">
                            <td>
                                <CButton
                                    color="danger"
                                    square
                                    size="sm"
                                    @click.prevent="updateUsuario(item)"
                                >
                                   <CIcon name="cil-trash"/>
                                </CButton>
                            </td>
                        </template>
                    </CDataTable>
            </CCol>
        </CRow>
        <hr>
    </div>
</template>

<script>
const items = [
  { nombreMedicamento: 'Motrin', descripcion: 'Motrin - Reduce el dolor de cabeza', compuestosDelMedicamento: 'A03 Shigelosis', dosisDelMedicamento: 'A038 Otras Shigelosis' },
  { nombreMedicamento: 'Advil', descripcion: 'Motrin - Bueno para la gripe', compuestosDelMedicamento: 'Ibuprofen', dosisDelMedicamento: 'Shigelosis' }
]

const fields = [
    { key: 'nombreMedicamento', label: 'Nombre del Medicamento' },
    { key: 'descripcion', label: 'Descripcion' },
    { key: 'compuestosDelMedicamento', label: 'Compuesto del Medicamento' },
    { key: 'dosisDelMedicamento', label: 'Dosis del Medicamento' },
    { key: 'borrarDiagnostico', label: 'Borrar', _style: 'width:1%', sorter: false, filter: false }
]

export default {
    data () {
        return {
            fields,
            items
        }
    }
}
</script>
