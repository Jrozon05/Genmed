<template>
    <div>
        <CAlert color="dark">
            <strong>EXPLORACION FISICA</strong>
        </CAlert>
        <CRow>
            <CCol sm="4">
                <CRow>
                    <CCol sm="12">
                        <CAlert color="info">
                            <strong>Exploración Cardiologico</strong>
                        </CAlert>
                    </CCol>
                </CRow>
                <CRow>
                <CCol sm="12" md="12">
                    <CRow>
                        <CCol xs="12" md="12">
                            <CRow class="form-group">
                                <CCol sm="6">
                                    <CInput
                                    label="Altura (cm)"
                                    placeholder=""
                                    />
                                </CCol>
                                <CCol sm="6">
                                    <CInput
                                    label="Peso (kg)"
                                    placeholder=""
                                    />
                                </CCol>
                            </CRow>
                            <CRow class="form-group">
                                <CCol sm="12">
                                    <CInput
                                    type="text"
                                    disabled
                                    >
                                    <template #append>
                                        <CButton type="submit" color="primary">Calcular Indice</CButton>
                                    </template>
                                    </CInput>
                                </CCol>
                            </CRow>
                        </CCol>
                    </CRow>
                </CCol>
            </CRow>
                <CRow>
                    <CCol sm="12" md="12">
                        <hr />
                        <CAlert color="info">
                            <strong>Signo Vitales</strong>
                        </CAlert>
                            <CRow>
                                <CCol xs="12" md="12">
                                    <CRow class="form-group">
                                        <CCol sm="3">
                                            <label for="">Brazo Izquierdo</label>
                                        </CCol>
                                        <CCol sm="3">
                                            <CInput
                                            placeholder="Acostado"
                                            />
                                        </CCol>
                                        <CCol sm="3">
                                            <CInput
                                            placeholder="Sentado"
                                            />
                                        </CCol>
                                        <CCol sm="3">
                                            <CInput
                                            placeholder="Parado"
                                            />
                                        </CCol>
                                    </CRow>
                                    <CRow class="form-group">
                                        <CCol sm="3">
                                            <label for="">Brazo Derecho</label>
                                        </CCol>
                                        <CCol sm="3">
                                            <CInput
                                            placeholder="Acostado"
                                            />
                                        </CCol>
                                        <CCol sm="3">
                                            <CInput
                                            placeholder="Sentado"
                                            />
                                        </CCol>
                                        <CCol sm="3">
                                            <CInput
                                            placeholder="Parado"
                                            />
                                        </CCol>
                                    </CRow>
                                    <CRow class="form-group">
                                        <CCol sm="6">
                                            <CInput
                                            label="Frecuencia Cardíaca (Lat/Min)"
                                            placeholder="F.C"
                                            />
                                        </CCol>
                                        <CCol sm="6">
                                            <CInput
                                            label="Frecuencia Respiratorio (Resp/Min)"
                                            placeholder="F.R"
                                            />
                                        </CCol>
                                    </CRow>
                                    <CRow class="form-group">
                                        <CCol sm="6">
                                            <CInput
                                            label="Temperatura (Celsius)"
                                            placeholder="Temperatura"
                                            />
                                        </CCol>
                                    </CRow>
                                </CCol>
                            </CRow>
                    </CCol>
                </CRow>
            </CCol>
            <CCol sm="8">
                <CRow>
                    <CCol sm="12">
                    <CCard accent-color="info">
                    <CCardHeader @click="cardCollapse = !cardCollapse" class="btn text-left">Exploración de Abdomen
                        <div class="card-header-actions">
                            <CLink class="card-header-action btn-minimize" @click="cardCollapse = !cardCollapse">
                            <CIcon :name="`cil-chevron-${cardCollapse ? 'bottom' : 'top'}`"/>
                            </CLink>
                        </div>
                    </CCardHeader>
                    <CCollapse :show="cardCollapse">
                    <CCardBody class="m-1">
                        <CRow>
                                <CCol xs="12" md="12">
                                    <CRow class="form-group">
                                        <CCol sm="6">
                                            <CInput
                                            label="Altura Uterina (cm)"
                                            placeholder=""
                                            />
                                        </CCol>
                                        <CCol sm="6">
                                            <CInput
                                            label=""
                                            placeholder="Sentado"
                                            />
                                        </CCol>
                                    </CRow>
                                </CCol>
                            </CRow>
                    </CCardBody>
                    </CCollapse>
                    </CCard>
                </CCol>
                </CRow>
            </CCol>
        </CRow>
        <hr>
    </div>
</template>

<script>
  export default {
      name: 'HistorialClinico',
      data () {
          return {
            cardCollapse: true,
            labelIcon: {
                labelOn: '\u2713',
                labelOff: '\u2715'
            },
            labelTxt: {
                labelOn: 'Lbs',
                labelOff: 'Kg'
            },
            text: `
                Anim pariatur cliche reprehenderit, enim eiusmod high life accusamus terry
                richardson ad squid. 3 wolf moon officia aute, non cupidatat skateboard dolor
                brunch. Food truck quinoa nesciunt laborum eiusmod. Brunch 3 wolf moon
                tempor, sunt aliqua put a bird on it squid single-origin coffee nulla
                assumenda shoreditch et. Nihil anim keffiyeh helvetica, craft beer labore
                wes anderson cred nesciunt sapiente ea proident. Ad vegan excepteur butcher
                vice lomo. Leggings occaecat craft beer farm-to-table, raw denim aesthetic
                synth nesciunt you probably haven't heard of them accusamus labore VHS.
            `
          }
      }
  }
</script>
