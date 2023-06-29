import { IProjectOverview } from "./project-overview"
import { IProjectPhoto } from "./project-photo.model";

export interface IProjectDetailed extends IProjectOverview {
  currentlyOpenProjectPhotos: IProjectPhoto[],
}