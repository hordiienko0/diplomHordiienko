import { AlertType } from "../types/alert-type";

export interface AlertModel {
    message: string,
    buttonText: string,
    type: AlertType
}