import { FilterOptionEnum } from 'src/app/core/enum/FilterOptionEnum';

export function compareDate(date: string, filter: number) {
  const givenDate = new Date(date);
  const now = new Date();
  if (filter === FilterOptionEnum.Today) {
    const today = new Date(
      `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`
    );
    const tomorrow = new Date(
      `${now.getMonth() + 1}/${now.getDate() + 1}/${now.getFullYear()}`
    );
    return givenDate >= today && givenDate < tomorrow;
  } else if (filter === FilterOptionEnum.This_Week) {
    const week = now.getDay();
    const weekStart = new Date(
      `${now.getMonth() + 1}/${now.getDate() - week}/${now.getFullYear()}`
    );
    const weekEnd = new Date(
      `${now.getMonth() + 1}/${now.getDate() + (7 - week)}/${now.getFullYear()}`
    );

    return givenDate >= weekStart && givenDate < weekEnd;
  } else if (filter === FilterOptionEnum.This_Month) {
    const thisMonthStart = new Date(
      `${now.getMonth() + 1}/01/${now.getFullYear()}`
    );
    const nextMonthStart = new Date(
      `${now.getMonth() === 11 ? '01' : now.getMonth() + 2}/01/${
        now.getMonth() === 11 ? now.getFullYear() + 1 : now.getFullYear()
      }`
    );

    return givenDate >= thisMonthStart && givenDate < nextMonthStart;
  }
  return true;
}
