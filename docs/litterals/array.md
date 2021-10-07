## Tableaux
# Initialisation

```c
  //     TYPE loop[666] = {LITTERAL, LITTERAL, LITTERAL, LITTERAL};
  loop[0] = 0x934eefb745859aebULL;
  loop[1] = 0x934eefb745859aebULL;
  loop[2] = 0x934eefb745859aebULL;
  loop[3] = 0x934eefb745859aebULL;
  tmp___0 = 4U;
  while (! (tmp___0 >= 666U)) {
    loop[tmp___0] = 275875246405016262ULL;
    tmp___0 ++;
  }
```

Pour l'initialisation explicite, les constantes sont obfusqués en déroulant l'assignation sur le tableau. Néanmoins, si on utilise l'initialisation implicite, une boucle est utilisée avec une même valeur pour tous les itérations.


