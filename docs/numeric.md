## Valeurs numériques
- Si on n'utilise pas des valeurs numériques (code mort), la valeur obfusquée n'est jamais réduite / simplifiée.
- Les constantes utiliséees par les transformations, 

# Poly1
- Les constantes utilisées dans l'expression affine (ax+b) sont non signées ou signées (en fonction du signe de la valeur originale) et elles sont une taille mémoire supérieur ou égales au type de base.
# XOR
 - Pas grand chose à dire.
# ADD
- Ne fonctionne que pour les types suivants 
  - [unsigned] int     
  - [unsigned] char 
  - [unsigned] short

Pour éviter les dépassements de capacité sur les plus grands entiers représentables, il ne les supporte pas. Sinon, il opère un transtypage. 