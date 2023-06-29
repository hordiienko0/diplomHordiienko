import { AlertType } from "../../modules/alert/resources/types/alert-type";

export interface NotificationDto {
  sender: string,
  action: string,
  message: string,
  type: AlertType
}
