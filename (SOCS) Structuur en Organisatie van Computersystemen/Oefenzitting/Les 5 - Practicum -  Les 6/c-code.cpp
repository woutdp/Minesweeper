int encryptAndCheck(int passwd[]){
  int i, j;
  int equal = 1;

  i = 0;
  while( i < MAXLENGTH){
    passwd[i] = passwd[i] + passwd[(i + passwd[i]) % MAXLENGTH];

    j = 0;
    while (j < MAXLENGTH){
      printint(passwd[j++]);
    }
    nieuweLijn();

    i++;
  }

  i = 0;
  while( (i < MAXLENGTH) && (equal == 1)){
    if( internalPasswd[i] != passwd[i] ){
      equal = 0;
    }
    i++;
  }
  return equal;
}