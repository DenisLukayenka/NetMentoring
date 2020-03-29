export enum DataTableType {
    Products = 0,
    Employees,
    RegionStat,
    EmployeeShippers,
    Categories,
}

export namespace DataTableType {

    export function values() {
      return Object.keys(DataTableType).filter(
        (type) => isNaN(<any>type) && type !== 'values'
      );
    }
  }