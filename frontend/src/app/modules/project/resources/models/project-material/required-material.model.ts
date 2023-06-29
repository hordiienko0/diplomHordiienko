export interface RequiredMaterial{
    id: number,
    amount: number,
    maxamount: number,
    materialTypeName: string,
    measurementName: string,
    projectId: number,
    buildingId: number,
    price: number
}