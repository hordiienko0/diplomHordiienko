export interface GetRequiredMaterialsDtoModel {
  requiredMaterials: GetRequiredMaterialDtoModel[]
}

export interface GetRequiredMaterialDtoModel {
  id: number
  amount: number
  measurement: string
  companyName: string
  companyAddress: string
  type: string
}
