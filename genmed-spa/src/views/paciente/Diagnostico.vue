<template>
    <div>
        <CAlert color="dark">
            <strong>DIAGNOSTICO</strong>
        </CAlert>
        <CRow>
            <CCol sm="4">
                <CRow>
                    <CCol sm="12">
                        <CAlert color="info">
                            <strong>Crear Diagnostico</strong>
                        </CAlert>
                    </CCol>
                </CRow>
                <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Buscar Diagnostico"
                        placeholder=""
                        />
                    </CCol>
                </CRow>
                <CRow>
                <CCol sm="6">
                    <CInput
                    label="Capítulo"
                    placeholder=""
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Grupos"
                    placeholder=""
                    />
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <CInput
                    label="Categorias"
                    placeholder=""
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="CIE"
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
                            <strong>Listado Diagnosticos</strong>
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
  { capitulo: 'I | Ciertas enfermedades infecciosas y parasitarias', grupos: 'I | Ciertas enfermedades infecciosas y parasitarias', categoria: 'A03 Shigelosis', cie: 'A038 Otras Shigelosis' },
  { capitulo: 'I | Ciertas enfermedades infecciosas y parasitarias', grupos: 'I | Ciertas enfermedades infecciosas y parasitarias', categoria: 'A03 Shigelosis', cie: 'A038 Otras Shigelosis' }
]

const fields = [
    { key: 'capitulo', label: 'Capítulo' },
    { key: 'grupos', label: 'Grupos' },
    { key: 'categoria', label: 'Categorias' },
    { key: 'cie', label: 'CIE' },
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
