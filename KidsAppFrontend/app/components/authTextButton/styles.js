import color from '@/app/styles/color';
import {StyleSheet} from 'react-native'

const styles = StyleSheet.create({
    container:{
        margin:5,
    },
    blurContainer: {
        justifyContent: 'center',
        alignItems: 'center',
        padding: 5,
        backgroundColor: 'rgba(255, 255, 255, 0.5)', 
      }, 
      text: {
        color: color.GirlColors.Primary, 
        fontSize: 14,
      },
});

export default styles