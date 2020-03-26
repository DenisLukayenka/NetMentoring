export enum DataTableType {
    Products,
    Employees,
    RegionStat,
    EmployeeShippers,
}

export namespace DataTableType {

    export function values() {
      return Object.keys(DataTableType).filter(
        (type) => isNaN(<any>type) && type !== 'values'
      );
    }
  }