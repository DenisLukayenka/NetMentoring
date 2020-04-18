export enum DomainRestriction {
    NotHigherCurrentDomain = 1,
    CurrentDomain = 2,
    NoRestriction = 4,
}

export namespace DomainRestriction {
    export function values() {
      return Object.keys(DomainRestriction).filter(
        (type) => isNaN(<any>type) && type !== 'values'
      );
    }
  }