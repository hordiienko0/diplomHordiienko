import {ICompanyOverview} from "./company-overview.model";
import {IMember} from "./member.model";
import {IService} from "../../../manage-resources/resources/models/service";
import {IMaterial} from "../../../manage-resources/resources/models/material-dto";
import {ICompanyProject} from "./company-project.model"

export interface ICompanyDetailed extends ICompanyOverview{
  email : string,
  members : IMember[]
  services: IService[]
  materials:IMaterial[]
  materialTotalCount:number
  projects: ICompanyProject[]
  projectsTotalCount: number
}
