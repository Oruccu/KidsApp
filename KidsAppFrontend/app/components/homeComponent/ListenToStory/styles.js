import color from '@/app/styles/color';
import { StyleSheet } from 'react-native'

const styles = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: color.GirlColors.Primary,
    borderRadius: 20,
    margin: 5,
    width: 170, 
    alignSelf: 'center',
    height:80,
    padding:5
  },
  image: {
    width: 70,
    height: 70,
    resizeMode:'contain'
  },
  title: {
    fontSize: 20,
    color: color.GirlColors.Secondary,
    flexWrap: 'wrap', 
    flex: 1,
    textAlign:'center'
  },
});

export default styles