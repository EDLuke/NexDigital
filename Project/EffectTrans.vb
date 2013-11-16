Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.ComponentModel

Friend Class EffectTrans
    Implements IDisposable

    'Evento EffectEnded: se dispara al finalizar un efecto
    Public Event EffectEnded As EventHandler

#Region " Enumeraciones "

    'Enumeración de Efectos
    Public Enum Efectos
        Abanico_Derecha = 0
        Abanico_Izquierda
        Aparecer
        Barras_Horizontales
        Barras_Verticales
        Barrido_Horizontal
        Barrido_Vertical
        Circulos_Dentro
        Circulos_Fuera
        Desplegar_Centro
        Desplegar_ID
        Desplegar_II
        Desplegar_SD
        Desplegar_SI
        Diagonal
        DivisiónH_Entrante
        DivisiónH_Saliente
        DivisiónV_Entrante
        DivisiónV_Saliente
        Empuja_División_Lados
        Empuja_División_Topes
        Empujar_Abajo
        Empujar_Arriba
        Empujar_Derecha
        Empujar_Diagonal_ID
        Empujar_Diagonal_II
        Empujar_Diagonal_SD
        Empujar_Diagonal_SI
        Empujar_Izquierda
        Estirar_Centro
        Estirar_ID
        Estirar_II
        Estirar_SD
        Estirar_SI
        Girar_Centro
        Girar_Espiral_Abajo
        Girar_Espiral_Arriba
        PersianasH_Abajo
        PersianasH_Arriba
        PersianasV_Derecha
        PersianasV_Izquierda
        Reloj
        Reloj_AntiHorario
        Rodar_DAbajo
        Rodar_DArriba
        Rodar_IAbajo
        Rodar_IArriba
        Rueda_2Ejes
        Rueda_3Ejes
        Rueda_4Ejes
        Rueda_8Ejes
        Simetrico_Adentro
        Simetrico_Afuera
        Simetrico_Derecha
        Simetrico_Izquierda
    End Enum

#End Region

#Region " Miembros Privados "

    'Velocidad a la que se dibujará el efecto
    Private _Velocidad As Integer = 20
    'Contiene el efecto actual
    Private _Efecto As Efectos = Efectos.Abanico_Derecha
    'Control donde se mostrarán los efectos
    Private _Contenedor As Control
    'Propiedad de _Contenedor donde se colocará una imagen para realizar los efectos
    Private _Propiedad As Reflection.PropertyInfo
    'Color que se usará para rellenar el fondo de la imagen
    Private _Color As Color = Color.Transparent
    'Control Timer que controlará la ejecución de los efectos
    Private _Tiempo As Timer
    'Cuenta las veces que se debe ejecutar el evento Tick para cada efecto
    Private _Contador As Integer = 0
    'Contiene una copia de la imagén pasada a uno de los constructores para realizar los efectos
    Private _bmpTextura As Image
    'Contiene una copia vacia de _bmpTextura donde se dibujará el efecto
    Private _bmpDibujar As Image
    'Creamos un objeto _Graphics para dibujar en _bmpDibujar
    Private _Gr As Graphics
    'Creamos un TextureBrush con la textura de la imágen _bmpTextura
    Private _Brocha As TextureBrush
    'Contienen el Ancho y Alto de _bmpTextura respectivamente
    Private _AnchoImagen, _AltoImagen As Single
    'Indica si actualmente hay un efecto ejecutandose
    Private blnEfectoEjecutandose As Boolean = False
    Private disposed As Boolean = False

#Region " Variables especificas de algunos efectos "

    'Ángulo de giro 
    Private _AnguloGiro As Single
    'Cantidad en que se aumentan las transformaciones de ejes.
    Private _xAumentaPos, _yAumentaPos As Single
    '_Aumento progresivo de los dibujos
    Private _Aumento As Single

#End Region

#End Region

#Region " Métodos Publicos "

    Public Sub New(ByVal RutaImagen As String, ByVal Contenedor As Control, ByVal strPropiedad As String, Optional ByVal intVelocidad As Integer = 20)
        Try
            _bmpTextura = Image.FromFile(RutaImagen)
            Iniciar(Contenedor, intVelocidad, strPropiedad)
        Catch ex As Exception When Not IO.Directory.Exists(RutaImagen)
            MessageBox.Show("La ruta de la imagen no es válida")
        Catch ex As Exception When _bmpTextura Is Nothing
            MessageBox.Show("La imagen no puede ser nula.")
        End Try
    End Sub

    Public Sub New(ByVal Imagen As Image, ByVal Contenedor As Control, ByVal strPropiedad As String, Optional ByVal intVelocidad As Integer = 20)
        Try
            _bmpTextura = Imagen
            Iniciar(Contenedor, intVelocidad, strPropiedad)
        Catch ex As Exception When _bmpTextura Is Nothing
            MessageBox.Show("La imagen no puede ser nula.")
        End Try
    End Sub

    'Ejecuta el efecto en la propiedad "EfectosTrans.EfectoActual"
    Public Overloads Sub Start()
        If Not disposed Then
            'Iniciamos los objetos de dibujo y el Timer
            EstableceObjetos()
        Else
            Throw New ObjectDisposedException("", "Esta instancia de la clase EfectosTrans ha sido desechada previamente y por lo tanto, ya no es accesible.")
        End If
    End Sub

    'Ejecuta el efecto pasado en eEfecto
    Public Overloads Sub Start(ByVal eEfecto As Efectos)
        If Not disposed Then
            _Efecto = eEfecto
            'Iniciamos los objetos de dibujo y el Timer
            EstableceObjetos()
        Else
            Throw New ObjectDisposedException("", "Esta instancia de la clase EfectosTrans ha sido desechada previamente y por lo tanto, ya no es accesible.")
        End If
    End Sub

#End Region

#Region " Métodos Privados "

    'Establecemos las variables de nivel de modulo
    Private Sub Iniciar(ByVal Contenedor As Control, ByVal intVelocidad As Integer, ByVal strPropiedad As String)
        _Contenedor = Contenedor
        _Velocidad = ValidaVelocidad(intVelocidad)

        _AnchoImagen = _bmpTextura.Width
        _AltoImagen = _bmpTextura.Height
        'Hay imagenes que tienen un formato de pixel
        'que no permite crear un objeto Graphics para dibujar sobre
        'ellas, por eso creamos una copia de la imagen, del mismo tamaño
        'y con un formato de pixel que no de problemas
        _bmpDibujar = New Bitmap(CInt(_AnchoImagen), CInt(_AltoImagen), PixelFormat.Format32bppArgb)

        'Obtenemos y utilizamos la propiedad de tipo Image a partir de strPropiedad
        ObtenerPropiedad(strPropiedad)
        _Tiempo = New Timer
        _Tiempo.Interval = 70
        'Controlador del evento Tick del objeto Timer
        AddHandler _Tiempo.Tick, AddressOf TiempoTick
    End Sub

    'El siguiente código me fue proporcionado por Eduardo A. Morcillo [MS MVP VB]
    Private Sub ObtenerPropiedad(ByVal strPropiedad As String)
        ' Obtengo el tipo de control
        Dim t As Type = _Contenedor.GetType
        ' Obtengo la propiedad
        _Propiedad = t.GetProperty(strPropiedad)
        ' Verifico que se devolvio la propiedad
        If _Propiedad Is Nothing Then
            Throw New ArgumentException("El control no posee la propiedad")
        End If
        ' Verifico que la propiedad devuelva 
        ' el tipo Image o una subclase de el
        If Not GetType(Drawing.Image).IsAssignableFrom(_Propiedad.PropertyType) Then
            Throw New ArgumentException("La propiedad no es de tipo Image")
        End If
    End Sub

    '(Re)Establece objetos y variables globales a su estado original antes de ejecutar un efecto
    Private Sub EstableceObjetos()
        Try
            'Si el efecto anterior todabia no terminó de ejecutarse salimos
            If blnEfectoEjecutandose Then Exit Sub
            'Iniciamos el objeto Graphics
            _Gr = Graphics.FromImage(_bmpDibujar)

            Select Case _Efecto
                Case Efectos.Aparecer, Efectos.Estirar_Centro To Efectos.Girar_Espiral_Arriba, Efectos.Rodar_DAbajo, Efectos.Rodar_DArriba
                    'Para estos efectos no se necesita el objeto _Brocha (se usa DrawImage)
                    'En el caso de 'Rodar_DArriba' y 'Rodar_DAbajo', 
                    'el código de estos efectos inicializan el objeto
                Case Else
                    'Iniciamos el objeto TextureBrush
                    IniTextureBrush()
            End Select

            '******* Aquí se modifica el contenedor de la imagen *******
            'Asignamos _bmpDibujar a la propiedad del Contenedor de la imagen
            _Propiedad.SetValue(_Contenedor, _bmpDibujar, Nothing)
            '******* Aquí se modifica el contenedor de la imagen *******

            'Limpiamos el contenido de _bmpDibujar
            _Gr.Clear(_Color)
            'Reiniciamos variables
            _Contador = 0
            _Aumento = 0
            _AnguloGiro = 0
            _xAumentaPos = 0
            _yAumentaPos = 0
            'True paar indicar que el efecto se va a iniciar
            blnEfectoEjecutandose = True
            'Iniciamos el objeto Timer
            _Tiempo.Start()
        Catch ex As Exception When _bmpDibujar Is Nothing
            Throw New NullReferenceException("No se ha establecido la imagen donde se dibujará el efecto")
        End Try
    End Sub

    'Inicializa el objeto TextureBrush
    Private Sub IniTextureBrush()
        _Brocha = New TextureBrush(_bmpTextura)
    End Sub

    'Controlador del evento Tick del control _Timer
    Private Sub TiempoTick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Ejecutamos cada 80 milesimas de segundo, el efecto selecionado
            Select Case _Efecto
                Case Efectos.Abanico_Derecha, Efectos.Abanico_Izquierda
                    Abanico(_Efecto)
                Case Efectos.Aparecer
                    Aparecer()
                Case Efectos.Barras_Horizontales, Efectos.Barras_Verticales
                    Barras(_Efecto)
                Case Efectos.Barrido_Horizontal, Efectos.Barrido_Vertical
                    Barrido(_Efecto)
                Case Efectos.Circulos_Fuera
                    Circulos_Fuera()
                Case Efectos.Circulos_Dentro
                    Circulos_Dentro()
                Case Efectos.Desplegar_Centro To Efectos.Desplegar_SI, Efectos.Estirar_Centro To Efectos.Estirar_SI
                    Desplegar_Estirar(_Efecto)
                Case Efectos.Diagonal
                    Diagonal()
                Case Efectos.DivisiónH_Entrante, Efectos.DivisiónH_Saliente
                    DivisionH(_Efecto)
                Case Efectos.DivisiónV_Entrante, Efectos.DivisiónV_Saliente
                    DivisionV(_Efecto)
                Case Efectos.Empujar_Abajo To Efectos.Empujar_Izquierda
                    Empujar(_Efecto)
                Case Efectos.Girar_Centro To Efectos.Girar_Espiral_Arriba
                    Girar(_Efecto)
                Case Efectos.PersianasH_Abajo, Efectos.PersianasH_Arriba
                    PersianasH(_Efecto)
                Case Efectos.PersianasV_Derecha, Efectos.PersianasV_Izquierda
                    PersianasV(_Efecto)
                Case Efectos.Reloj, Efectos.Reloj_AntiHorario
                    Reloj(_Efecto)
                Case Efectos.Simetrico_Adentro To Efectos.Simetrico_Izquierda
                    Simetrico(_Efecto)
                Case Efectos.Rueda_2Ejes To Efectos.Rueda_8Ejes
                    RuedaMultiple(_Efecto)
                Case Efectos.Empuja_División_Lados, Efectos.Empuja_División_Topes
                    EmpujaDivision(_Efecto)
                Case Efectos.Rodar_DAbajo To Efectos.Rodar_IArriba
                    Rodar(_Efecto)
            End Select
        Catch ex As System.Exception
            Throw New System.Exception(ex.ToString)
        End Try
    End Sub

    'Descarga objetos y detiene el Timer
    Private Sub DescargaObjetos()
        _Gr.Dispose()
        If Not (_Brocha Is Nothing) Then
            _Brocha.Dispose()
        End If
        _Tiempo.Stop()
        'False para indicar que el efecto acaba de terminar
        blnEfectoEjecutandose = False
        'El efecto terminó, disparamos un evento para indicarlo
        RaiseEvent EffectEnded(Me, New EventArgs)
    End Sub

    'Valida la propiedad VelocidadEfecto
    Private Function ValidaVelocidad(ByVal intValor As Integer) As Integer
        If intValor <= 0 Then
            Return _Velocidad ' Valor por defecto (20)
        ElseIf intValor > 100 Then
            Return 100
        Else : Return intValor
        End If
    End Function

#Region " Efectos de Transición "

    'Genera los efecto de abanico
    Private Sub Abanico(ByVal Efecto As Efectos)
        'Calculamos por Pitagoras el radio de la circunferencia con centro en (_AnchoImagen / 2, _AltoImagen) 
        'que dibujará la imagen.
        Dim Radio As Single = CSng(Math.Pow(((_AnchoImagen / 2) ^ 2) + (_AltoImagen ^ 2), 1 / 2))
        'Negativo: dirección del efecto
        Dim AnguloInicio, Negativo As Integer

        'iniciamos variables según el efecto
        If Efecto = Efectos.Abanico_Derecha Then
            AnguloInicio = 180
            Negativo = 1
        ElseIf Efecto = Efectos.Abanico_Izquierda Then
            AnguloInicio = 0
            Negativo = -1
        End If
        'Llenamos media torta con centro en (_AnchoImagen / 2, _AltoImagen),
        'con la textura de la imágen original,
        'agregandole trozos de _AnguloGiro
        Dim Rec As New Rectangle(CInt(-(Radio - (_AnchoImagen / 2))), CInt(-(Radio - _AltoImagen)), CInt(Radio * 2), CInt(Radio * 2))
        'Mostramos el trozo actual de la imagen
        _Gr.FillPie(_Brocha, Rec, AnguloInicio, Negativo * _AnguloGiro)
        'Aumentamos el angulo de giro
        _AnguloGiro += CSng(180 / _Velocidad)
        'Mostramos los trozos actuales
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1
        'veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            DescargaObjetos()
        End If
    End Sub

    'Genera el efecto de desvanecimiento de la imagen
    Private Sub Aparecer()
        'Opacidad contiene el valor de opacidad actual con que se va a dibujar de la imagen
        Static Opacidad As Single = 0.0F
        Dim CopiaVelocidad As Single = 0.0F
        'Cm representa una matriz de 5X5 que servirá para modificar la opacidad de la imagen
        Dim Cm As ColorMatrix = New ColorMatrix
        'Con Ia establecemos la matriz de color Cm para luego usar Ia al dibujar la imagen mediante DrawImage
        Dim Ia As ImageAttributes = New ImageAttributes

        '100 es el valor maximo de la propiedad VelocidadEfecto
        'Para calcular la velocidad en función de esta propiedad calculamos CSng(101 - _Velocidad) / 1000
        CopiaVelocidad = CSng(101 - _Velocidad) / 1000
        'Aumentamos el valor de Opacidad
        Opacidad += CopiaVelocidad
        'El valor de Opacidad debe estar entre 0.0 y 1.0
        If Opacidad > 1.0F Then
            Opacidad = 1.0F
        ElseIf Opacidad < 0.01F Then
            Opacidad = 0.01F
            CopiaVelocidad = 0.01F
        End If
        'Limpiamos la imagen
        _Gr.Clear(_Color)
        'Cambiando el valor de la columna 3, fila 3 de la matriz de color se logra modificar la opacidad de la imagen
        Cm.Matrix33 = Opacidad
        'Establecemos a Cm como la matriz de color de el objeto de atributos de imagen Ia
        Ia.SetColorMatrix(Cm)
        'Dibujamos la imagen usando el objeto de atributos de imagen que preparamos previamente
        _Gr.DrawImage(_bmpTextura, New Rectangle(0, 0, CInt(_AnchoImagen), CInt(_AltoImagen)), 0, 0, _AnchoImagen, _AltoImagen, GraphicsUnit.Pixel, Ia)
        'Refrescamos el contenedor de la imagen
        _Contenedor.Refresh()
        'Cuando la imagen sea totalmente opaca terminamos el efecto
        If Opacidad >= 1.0F Then
            Opacidad = 0
            Ia.Dispose()
            DescargaObjetos()
        End If
    End Sub

    'Genera los efectos con Barras
    Private Sub Barras(ByVal Efecto As Efectos)
        Dim i, j, MedidaImagen, X, Y, Ancho, Alto As Integer
        Dim Iteraciones As Integer
        Dim _CopiaVelocidad As Integer

        'Establecemos variables según el efecto
        If Efecto = Efectos.Barras_Horizontales Then
            Ancho = CInt(_AnchoImagen)
            Alto = 2
            MedidaImagen = CInt(_AltoImagen)
        ElseIf Efecto = Efectos.Barras_Verticales Then
            Ancho = 2
            Alto = CInt(_AltoImagen)
            MedidaImagen = CInt(_AnchoImagen)
        End If
        'Calculamos en Iteraciones, la cantidad de barras que se van a dibujar
        'teniendo en cuenta que la medida de cada barra es de 2 y 
        'cuidando que todo quede en función de _Velocidad.
        'Aproximamos a el entero más grande en el caso de que la 
        'Medida de la imagen no sea divisible entre _Velocidad * 2
        Iteraciones = CInt(Math.Ceiling(MedidaImagen / (_Velocidad * 2)))
        _CopiaVelocidad = CInt(Math.Ceiling(MedidaImagen / (Iteraciones * 2)))

        _Tiempo.Stop()
        'Cada vez que se ejecuta este método dibujamos Iteraciones barras
        For i = 0 To Iteraciones - 1
            'Hallamos las coordenadas de cada barra que se necesita dibujar (sin repetir)
            'llamando a DameParUnico.
            j = DameParUnico(CInt(MedidaImagen - 2), CInt(MedidaImagen / 2) - 1)
            'Cuando j = -1 ya se dibujaron todas las barras (toda la imagen)
            If j = -1 Then Exit For
            If Efecto = Efectos.Barras_Horizontales Then
                X = 0 : Y = j
            ElseIf Efecto = Efectos.Barras_Verticales Then
                X = j : Y = 0
            End If
            'Dibujamos la barra actual
            _Gr.FillRectangle(_Brocha, New Rectangle(X, Y, Ancho, Alto))
        Next
        _Tiempo.Start()
        'Refrescamos el contenedor de la imagen
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _CopiaVelocidad veses terminamos el efecto
        If _Contador = _CopiaVelocidad Then
            DescargaObjetos()
        End If
    End Sub

    'Genera los efectos de Barrido
    Private Sub Barrido(ByVal Efecto As Efectos)

        'Creamos un segundo objeto Graphics para dibujar en _bmpDibujar
        Static Gr2 As Graphics

        Dim Persiana As Single
        Dim xEsquinaPersiana, yEsquinaPersiana As Single
        Dim AnchoPersiana, AltoPersiana As Single

        If Efecto = Efectos.Barrido_Horizontal Then
            _xAumentaPos = _AnchoImagen
            _yAumentaPos = 0
            xEsquinaPersiana = 0
            yEsquinaPersiana = _AltoImagen / 20
            AnchoPersiana = _AnchoImagen
            AltoPersiana = _AltoImagen / 20
        ElseIf Efecto = Efectos.Barrido_Vertical Then
            _xAumentaPos = 0
            _yAumentaPos = _AltoImagen
            xEsquinaPersiana = _AnchoImagen / 20
            yEsquinaPersiana = 0
            AnchoPersiana = _AnchoImagen / 20
            AltoPersiana = _AltoImagen
        End If

        If _Contador = 0 Then
            Gr2 = Graphics.FromImage(_bmpDibujar)
            'Movemos el centro del eje de coordenadas la primera vez
            'que se ejecuta el evento, para que la imagen se dibuje pero no se vea
            _Gr.TranslateTransform(_xAumentaPos, _yAumentaPos)
            Gr2.TranslateTransform(-_xAumentaPos, -_yAumentaPos)
        End If
        '***** Esto solo es necesario para imagenes con fondo transparente *****
        '_Gr.Clear(_Color)
        '***********************************************************************
        For Persiana = 0 To 20 'Nº de Persianas (20)
            'Las persianas pares las dibujamos con _Gr,
            'y las impares con Gr2
            Dim SupIzquierda As New PointF(Persiana * xEsquinaPersiana, Persiana * yEsquinaPersiana)
            Dim TamañoPersiana As New SizeF(AnchoPersiana, AltoPersiana)

            If Persiana Mod 2 = 0 Then
                Dim RecBarridoPar As New RectangleF(SupIzquierda, TamañoPersiana)
                _Gr.FillRectangle(_Brocha, RecBarridoPar)
            Else
                Dim RecBarridoImpar As New RectangleF(SupIzquierda, TamañoPersiana)
                Gr2.FillRectangle(_Brocha, RecBarridoImpar)
            End If
        Next
        'Transladamos los ejes progresivamente
        _Gr.TranslateTransform(-_xAumentaPos / _Velocidad, -_yAumentaPos / _Velocidad) 'Velocidad
        Gr2.TranslateTransform(_xAumentaPos / _Velocidad, _yAumentaPos / _Velocidad)
        'Contamos las veces que movimos la imágen
        _Contador += 1
        'Refrescamos el Control
        _Contenedor.Refresh()
        'Cuando el evento se halla ejecutado _Velocidad + 1
        'veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            Gr2.Dispose()
            xEsquinaPersiana = 0 : yEsquinaPersiana = 0
            AnchoPersiana = 0 : AltoPersiana = 0
            DescargaObjetos()
        End If
    End Sub

    'Genera el efecto con Circulos hacia Adentro
    Private Sub Circulos_Dentro()
        'El diametro de los circulos
        Dim Diametro As Single
        'Contiene los cuadrados pares
        Static RecAnterior As RectangleF
        'La diagonal de la imágen va a ser el diametro máximo. 
        'La calculamos por Pitagoras.
        Dim Diagonal As Single = CSng(Math.Pow((_AltoImagen ^ 2) + (_AnchoImagen ^ 2), 1 / 2))
        'Aumentamos el diametro
        Diametro = Diagonal - (_Contador * Diagonal / _Velocidad)
        'Definimos las coordenadas del punto superior
        'izquierdo del rectángulo a partir del cual
        'se dibujará un circulo circunscripto en él, de modo que 
        'el centro de el circulo coincida con el centro del Control
        Dim xEsquina As Single = (_AnchoImagen / 2) - (Diametro / 2)
        Dim yEsquina As Single = (_AltoImagen / 2) - (Diametro / 2)
        Dim MiPunto As New PointF(xEsquina, yEsquina)
        'Tamaño del rectángulo donde se dibujará el circulo
        Dim Tamaño As SizeF = New SizeF(Diametro, Diametro)
        Dim Rectángulo As New RectangleF(MiPunto, Tamaño)

        If _Contador > 0 Then
            Dim Trayecto1 As New GraphicsPath
            'Usamos el rectángulo anterior para que no queden zonas sin dibujar
            Trayecto1.AddEllipse(RecAnterior)
            Dim Trayecto2 As New GraphicsPath
            Trayecto2.AddEllipse(Rectángulo)
            'Creamos un objeto region
            'compuesto por la elipse encapsuladas en Trayecto1
            'Al añadir el GraphicsPth Trayecto2
            'indicamos que vamos a combinar el contenido
            'del nuevo GraphicsPath Trayecto2 con Trayecto1 usando el método Xor
            Dim Región As New Region(Trayecto2)
            Región.Xor(Trayecto1)
            'Dibujamos la región
            If _Contador > 0 Then _Gr.FillRegion(_Brocha, Región)
            'Mostramos el circulo actual
            _Contenedor.Refresh()
        End If
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Recordamos el último rectangulo que se uso para dibujar el último circulo
        RecAnterior = Rectángulo
        'Cuando el evento se halla ejecutado _Velocidad + 1
        'veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            RecAnterior = Nothing
            DescargaObjetos()
        End If
    End Sub

    'Genera el efecto con Circulos hacia Afuera
    Private Sub Circulos_Fuera()
        'El diametro de los circulos
        Static Diametro As Single
        'La diagonal de la imágen va a ser el diametro máximo. 
        'La calculamos por Pitagoras.
        Dim Diagonal As Single = CSng(Math.Pow((_AltoImagen ^ 2) + (_AnchoImagen ^ 2), 1 / 2))
        'Definimos las coordenadas del punto superior
        'izquierdo del rectángulo a partir del cual
        'se dibujará un circulo circunscripto en él, de modo que 
        'el centro de el circulo coincida con el centro del Control
        Dim xEsquina As Single = (_AnchoImagen / 2) - (Diametro / 2)
        Dim yEsquina As Single = (_AltoImagen / 2) - (Diametro / 2)
        Dim MiPunto As New PointF(xEsquina, yEsquina)
        'Tamaño del rectángulo donde se dibujará el circulo
        Dim Tamaño As SizeF = New SizeF(Diametro, Diametro)
        Dim Rectángulo As New RectangleF(MiPunto, Tamaño)
        _Gr.FillEllipse(_Brocha, Rectángulo)
        'Mostramos el circulo actual
        _Contenedor.Refresh()
        'Aumentamos el diametro
        Diametro += Diagonal / _Velocidad
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1
        'veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            Diametro = 0
            DescargaObjetos()
        End If

    End Sub

    'Genera los efectos de despliegue y estiramiento
    Private Sub Desplegar_Estirar(ByVal Efecto As Efectos)
        Dim X, Y, Ancho, Alto As Single

        _Aumento += CSng(1 / _Velocidad)
        'Iniciamos variables según el efecto.
        'Notar que el código para los efectos "Desplegar" y "Estirar" son identicos 
        'excepto por el método usado para dibujar la imagen (FillRectangle o DrawImage)
        Select Case Efecto
            Case Efectos.Desplegar_Centro, Efectos.Estirar_Centro
                X = (_AnchoImagen / 2) - ((_AnchoImagen / 2) * _Aumento)
                Y = (_AltoImagen / 2) - ((_AltoImagen / 2) * _Aumento)
            Case Efectos.Desplegar_ID, Efectos.Estirar_ID
                X = _AnchoImagen - (_AnchoImagen * _Aumento)
                Y = _AltoImagen - (_AltoImagen * _Aumento)
            Case Efectos.Desplegar_II, Efectos.Estirar_II
                X = 0
                Y = _AltoImagen - (_AltoImagen * _Aumento)
            Case Efectos.Desplegar_SD, Efectos.Estirar_SD
                X = _AnchoImagen - (_AnchoImagen * _Aumento)
                Y = 0
            Case Efectos.Desplegar_SI, Efectos.Estirar_SI
                X = 0
                Y = 0
        End Select

        Ancho = _AnchoImagen * _Aumento
        Alto = _AltoImagen * _Aumento

        Dim Rec As New RectangleF(X, Y, Ancho, Alto)
        _Gr.Clear(_Color)

        Select Case Efecto
            Case Efectos.Desplegar_Centro To Efectos.Desplegar_SI
                _Gr.FillRectangle(_Brocha, Rec)
            Case Efectos.Estirar_Centro To Efectos.Estirar_SI
                _Gr.DrawImage(_bmpTextura, Rec)
        End Select
        'Refrescamos el contenedor de la imagen
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad veses terminamos el efecto
        If _Contador = _Velocidad Then
            DescargaObjetos()
        End If
    End Sub

    'Genera el efecto en Diagonal desde el angúlo superior izquierdo
    Private Sub Diagonal()
        'Aumentamos los lados del triángulo
        _xAumentaPos += _AnchoImagen / _Velocidad
        _yAumentaPos += _AltoImagen / _Velocidad
        'Dibujanos el triángulo actual
        _Gr.FillPolygon(_Brocha, New PointF() {New PointF(0, 0), New PointF(_xAumentaPos, 0), New PointF(0, _yAumentaPos)})
        'Actualizamos el dibujo
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad * 2
        'veses terminamos el efecto
        If _Contador = _Velocidad * 2 Then
            DescargaObjetos()
        End If
    End Sub

    'Genera los efectos de Divición Horizontal
    Private Sub DivisionH(ByVal Efecto As Efectos)

        Dim Velocidad As Single = _AnchoImagen / _Velocidad
        'Si es la primera vez que se llega aquí, establecemos algunas variables
        If _Contador = 0 Then

            If Efecto = Efectos.DivisiónH_Entrante Then
                _Aumento = 0
            ElseIf Efecto = Efectos.DivisiónH_Saliente Then
                _Aumento = _AnchoImagen / 2
            End If
        End If

        Dim RecDivision As New RectangleF(_Aumento, 0, Velocidad, _AltoImagen)
        Dim RecDivision2 As New RectangleF(_AnchoImagen - _Aumento, 0, Velocidad, _AltoImagen)
        'Dibujamos los rectangulos de división
        _Gr.FillRectangle(_Brocha, RecDivision)
        _Gr.FillRectangle(_Brocha, RecDivision2)

        _Contenedor.Refresh()
        'Aumentamos el tamaño de los rectangulos
        _Aumento += Velocidad
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado la mitad de la cantidad de
        'diviciones del Ancho + 1 veses terminamos el efecto
        If _Contador = ((_Velocidad + 1) \ 2) + 1 Then
            DescargaObjetos()
        End If

    End Sub

    'Genera los efectos de Divición Vertical
    Private Sub DivisionV(ByVal Efecto As Efectos)

        Dim Velocidad As Single = _AltoImagen / _Velocidad
        'Si es la primera vez que se llega aquí, establecemos algunas variables
        If _Contador = 0 Then
            If Efecto = Efectos.DivisiónV_Entrante Then
                _Aumento = 0
            ElseIf Efecto = Efectos.DivisiónV_Saliente Then
                _Aumento = _AltoImagen / 2
            End If
        End If

        Dim RecDivision As New RectangleF(0, _Aumento, _AnchoImagen, Velocidad)
        Dim RecDivision2 As New RectangleF(0, _AltoImagen - _Aumento, _AnchoImagen, Velocidad)
        'Dibujamos los rectangulos de división
        _Gr.FillRectangle(_Brocha, RecDivision)
        _Gr.FillRectangle(_Brocha, RecDivision2)

        _Contenedor.Refresh()
        'Aumentamos el tamaño de los rectangulos
        _Aumento += Velocidad
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado la mitad de la cantidad de
        'diviciones del Alto + 1 veses terminamos el efecto
        If _Contador = ((_Velocidad + 1) \ 2) + 1 Then
            DescargaObjetos()
        End If

    End Sub

    'Genera los efectos de Empuje
    Private Sub Empujar(ByVal eEfecto As Efectos)
        'Si es la primera vez que se llega aquí, 
        'Establecemos las transformaciónes iniciales
        'según la variante de efecto de Empuje
        If _Contador = 0 Then
            If eEfecto = Efectos.Empujar_Abajo Then
                _Gr.TranslateTransform(0, -_AltoImagen)
                _xAumentaPos = 0 : _yAumentaPos = _AltoImagen / _Velocidad
            ElseIf eEfecto = Efectos.Empujar_Arriba Then
                _Gr.TranslateTransform(0, _AltoImagen)
                _xAumentaPos = 0 : _yAumentaPos = -_AltoImagen / _Velocidad
            ElseIf eEfecto = Efectos.Empujar_Derecha Then
                _Gr.TranslateTransform(-_AnchoImagen, 0)
                _xAumentaPos = _AnchoImagen / _Velocidad : _yAumentaPos = 0
            ElseIf eEfecto = Efectos.Empujar_Izquierda Then
                _Gr.TranslateTransform(_AnchoImagen, 0)
                _xAumentaPos = -_AnchoImagen / _Velocidad : _yAumentaPos = 0
            ElseIf eEfecto = Efectos.Empujar_Diagonal_SI Then
                _Gr.TranslateTransform(-_AnchoImagen, -_AltoImagen)
                _xAumentaPos = _AnchoImagen / _Velocidad : _yAumentaPos = _AltoImagen / _Velocidad
            ElseIf eEfecto = Efectos.Empujar_Diagonal_SD Then
                _Gr.TranslateTransform(_AnchoImagen, -_AltoImagen)
                _xAumentaPos = -_AnchoImagen / _Velocidad : _yAumentaPos = _AltoImagen / _Velocidad
            ElseIf eEfecto = Efectos.Empujar_Diagonal_II Then
                _Gr.TranslateTransform(-_AnchoImagen, _AltoImagen)
                _xAumentaPos = _AnchoImagen / _Velocidad : _yAumentaPos = -_AltoImagen / _Velocidad
            ElseIf eEfecto = Efectos.Empujar_Diagonal_ID Then
                _Gr.TranslateTransform(_AnchoImagen, _AltoImagen)
                _xAumentaPos = -_AnchoImagen / _Velocidad : _yAumentaPos = -_AltoImagen / _Velocidad
            End If
        End If
        '***** Esto solo es necesario para imagenes con fondo transparente *****
        '_Gr.Clear(_Color)
        '***********************************************************************
        'Dibujamos la imagen con las transformaciónes de ejes. 
        Dim RecEmpuja As New RectangleF(0, 0, _AnchoImagen, _AltoImagen)
        _Gr.FillRectangle(_Brocha, RecEmpuja)
        _Gr.TranslateTransform(_xAumentaPos, _yAumentaPos)
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1 veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            DescargaObjetos()
        End If

    End Sub

    'Genera los efectos de Empuje con División
    Private Sub EmpujaDivision(ByVal Efecto As Efectos)
        'Creamos el segundo objeto Graphics para dibujar en _bmpDibujar
        Static Gr2 As Graphics
        Dim Ancho, Alto, xEsquina, yEsquina As Single

        If Efecto = Efectos.Empuja_División_Lados Then
            _xAumentaPos = _AnchoImagen / (2 * _Velocidad)
            _yAumentaPos = 0
            Ancho = _AnchoImagen / 2
            Alto = _AltoImagen
            xEsquina = Ancho
            yEsquina = 0
        ElseIf Efecto = Efectos.Empuja_División_Topes Then
            _xAumentaPos = 0
            _yAumentaPos = _AltoImagen / (2 * _Velocidad)
            Ancho = _AnchoImagen
            Alto = _AltoImagen / 2
            xEsquina = 0
            yEsquina = Alto
        End If
        'Si es la primera vez que se llega aquí,
        'Establecemos las transformaciónes iniciales
        'según la variante de efecto de Empuje y División
        If _Contador = 0 Then
            Gr2 = Graphics.FromImage(_bmpDibujar)
            If Efecto = Efectos.Empuja_División_Lados Then
                _Gr.TranslateTransform(-Ancho, 0)
                Gr2.TranslateTransform(Ancho, 0)
            ElseIf Efecto = Efectos.Empuja_División_Topes Then
                _Gr.TranslateTransform(0, -Alto)
                Gr2.TranslateTransform(0, Alto)
            End If
        End If

        Dim RecEmpujaLado As New RectangleF(0, 0, Ancho, Alto)
        Dim RecEmpujaOpuesto As New RectangleF(xEsquina, yEsquina, Ancho, Alto)
        '***** Esto solo es necesario para imagenes con fondo transparente *****
        '_Gr.Clear(_Color)
        '***********************************************************************
        'Dibujamos los rectangulos y los lenamos con la imagen   
        _Gr.FillRectangle(_Brocha, RecEmpujaLado)
        Gr2.FillRectangle(_Brocha, RecEmpujaOpuesto)
        'Movemos los ejes de coordenadas
        _Gr.TranslateTransform(_xAumentaPos, _yAumentaPos)
        Gr2.TranslateTransform(-_xAumentaPos, -_yAumentaPos)
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1 veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            Gr2.Dispose()
            DescargaObjetos()
        End If

    End Sub

    'm: Matriz que vamos a usar para establecer tranformaciones en algunos efectos
    Private m As Matrix
    'Genera los efectos de giro de la imagen con movimiento en espiral
    Private Sub Girar(ByVal Efecto As Efectos)
        Dim X, Y, Ancho, Alto As Single

        If _Contador = 0 Then
            'La primera vez que se ejecuta el procedimiento, iniciamos la martix
            'y establecemos el ángulo de giro
            m = New Matrix
            _AnguloGiro = CInt(360 / _Velocidad)
        End If

        _Aumento += CSng(1 / _Velocidad)

        Ancho = _AnchoImagen * _Aumento
        Alto = _AltoImagen * _Aumento
        If Efecto = Efectos.Girar_Centro Then
            X = (_AnchoImagen / 2) - ((_AnchoImagen / 2) * _Aumento)
            Y = (_AltoImagen / 2) - ((_AltoImagen / 2) * _Aumento)
        ElseIf Efecto = Efectos.Girar_Espiral_Abajo Then
            X = 0
            Y = _AltoImagen - _AltoImagen * _Aumento
        ElseIf Efecto = Efectos.Girar_Espiral_Arriba Then
            X = _AnchoImagen - _AnchoImagen * _Aumento
            Y = 0
        End If
        'Rectangulo donde se dibujará la imagen
        Dim Rec As New RectangleF(X, Y, Ancho, Alto)
        _Gr.Clear(_Color)
        'Dibujamos la imagen completa
        _Gr.DrawImage(_bmpTextura, Rec)

        '"Preparamos" una rotación con centro en el centro de la imagen en la matriz m
        'por medio del metodo RotateAt y estableciendo la transformación en nuestro objeto Graphics _Gr
        'para ser usada en la proxima ejecución del procedimiento
        m.RotateAt(_AnguloGiro, New PointF(_AnchoImagen / 2, _AltoImagen / 2))
        _Gr.Transform = m

        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Para asegurarnos de que la imagen de 1 vuelta entera terminamos el efecto cuando _Contador = 360,
        'o sea ...
        If _Contador = CInt(360 / _AnguloGiro) Then
            '***** Esto solo es necesario para imagenes con fondo transparente *****
            '_Gr.Clear(_Color)
            '***********************************************************************
            'Rotamos la imagen a su posición original y la dibujamos antes de descargar todo
            m.RotateAt(-CSng(_AnguloGiro * _Contador), New PointF(_AnchoImagen / 2, _AltoImagen / 2))
            _Gr.Transform = m
            _Gr.DrawImage(_bmpTextura, _bmpDibujar.GetBounds(GraphicsUnit.Pixel))
            _Contenedor.Refresh()
            m.Dispose()
            DescargaObjetos()
        End If
    End Sub

    'Genera los efectos con Persianas Horizontales
    Private Sub PersianasH(ByVal Efecto As Efectos)

        Dim Persiana As Integer
        'Total de Persianas
        Dim TotalPersianas As Integer = 15
        Dim Velocidad As Integer = TotalPersianas * _Velocidad
        'Si es la primera vez que se llega aquí, establecemos algunas variables
        If _Contador = 0 Then
            If Efecto = Efectos.PersianasH_Abajo Then
                _Aumento = 0
            ElseIf Efecto = Efectos.PersianasH_Arriba Then
                _Aumento = _AltoImagen / TotalPersianas
            End If
        End If

        For Persiana = 0 To TotalPersianas - 1
            'Dibuja un trozo de cada una de las persianas por cada ejecución del evento
            Dim SupIzquierda As New PointF(0, _Aumento + (Persiana * (_AltoImagen / TotalPersianas)))
            Dim TamañoPersiana As New SizeF(_AnchoImagen, _AltoImagen / Velocidad)
            Dim RecPersiana As New RectangleF(SupIzquierda, TamañoPersiana)
            _Gr.FillRectangle(_Brocha, RecPersiana)
        Next
        _Contenedor.Refresh()

        'Aumentamos el tamaño de cada persiana para la próxima vez que se dibujen
        If Efecto = Efectos.PersianasH_Abajo Then
            _Aumento += _AltoImagen / Velocidad
        ElseIf Efecto = Efectos.PersianasH_Arriba Then
            _Aumento -= _AltoImagen / Velocidad
        End If
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1 veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            DescargaObjetos()
        End If

    End Sub

    'Genera los efectos con Persianas Verticales
    Private Sub PersianasV(ByVal Efecto As Efectos)

        Dim Persiana As Integer
        Dim TotalPersianas As Integer = 15
        Dim Velocidad As Integer = TotalPersianas * _Velocidad
        'Si es la primera vez que se llega aquí, establecemos algunas variables
        If _Contador = 0 Then
            If Efecto = Efectos.PersianasV_Izquierda Then
                _Aumento = 0
            ElseIf Efecto = Efectos.PersianasV_Derecha Then
                _Aumento = _AnchoImagen / TotalPersianas
            End If
        End If

        For Persiana = 0 To TotalPersianas - 1
            'Dibuja un trozo de cada una de las persianas por cada ejecución del evento
            Dim SupIzquierda As New PointF(_Aumento + (Persiana * (_AnchoImagen / TotalPersianas)), 0)
            Dim TamañoPersiana As New SizeF(_AnchoImagen / Velocidad, _AltoImagen)
            Dim RecPersiana As New RectangleF(SupIzquierda, TamañoPersiana)
            _Gr.FillRectangle(_Brocha, RecPersiana)
        Next
        _Contenedor.Refresh()
        'Aumentamos el tamaño de cada persiana para la próxima vez que se dibujen
        If Efecto = Efectos.PersianasV_Izquierda Then
            _Aumento += _AnchoImagen / Velocidad
        ElseIf Efecto = Efectos.PersianasV_Derecha Then
            _Aumento -= _AnchoImagen / Velocidad
        End If
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1 veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            DescargaObjetos()
        End If

    End Sub

    'Genera el efecto de Reloj
    Private Sub Reloj(ByVal Efecto As Efectos)
        'La diagonal de la imágen va a ser el diametro. 
        'La calculamos por Pitagoras.
        Dim Diagonal As Single = CSng(Math.Pow((_AltoImagen ^ 2) + (_AnchoImagen ^ 2), 1 / 2))
        'Negativo: indica la dirección del giro
        Dim Negativo As Integer

        If Efecto = Efectos.Reloj Then
            Negativo = 1
        ElseIf Efecto = Efectos.Reloj_AntiHorario Then
            Negativo = -1
        End If
        'Llenamos una torta con centro en el centro del Control,
        'con la textura de la imágen original,
        'agregandole trozos de _AnguloGiro
        _Gr.FillPie(_Brocha, CSng(-(Diagonal - _AnchoImagen) / 2), CSng(-(Diagonal - _AltoImagen) / 2), Diagonal, Diagonal, -90, Negativo * _AnguloGiro)
        'Mostramos el trozo actual
        _Contenedor.Refresh()
        _AnguloGiro += CSng(360 / _Velocidad)
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1
        'veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            DescargaObjetos()
        End If
    End Sub

    'Genera los efectos de Rodar
    Private Sub Rodar(ByVal Efecto As Efectos)
        'intAngulo: angulo de giro progresivo
        Dim intAngulo As Single = CSng(90 / _Velocidad)

        If _Contador = 0 Then
            'Si es la primera vez que se llega aquí iniciamos variables dependiendo
            'de el tipo de efecto
            If Efecto = Efectos.Rodar_IAbajo Then
                _AnguloGiro = 90.0F
                _Aumento = -1
                _xAumentaPos = 0
                _yAumentaPos = 0
            ElseIf Efecto = Efectos.Rodar_IArriba Then
                _AnguloGiro = -90.0F
                _Aumento = 1
                _xAumentaPos = 0
                _yAumentaPos = 0
            ElseIf Efecto = Efectos.Rodar_DArriba Then
                _bmpTextura.RotateFlip(RotateFlipType.RotateNoneFlipXY)
                IniTextureBrush()
                _AnguloGiro = 270
                _Aumento = -1
                _xAumentaPos = _AnchoImagen
                _yAumentaPos = _AltoImagen
            ElseIf Efecto = Efectos.Rodar_DAbajo Then
                _bmpTextura.RotateFlip(RotateFlipType.RotateNoneFlipXY)
                IniTextureBrush()
                _AnguloGiro = -270.0F
                _Aumento = 1
                _xAumentaPos = _AnchoImagen
                _yAumentaPos = _AltoImagen
            End If
            'Movemos el centro del eje de coordenadas la primera vez
            'que se ejecuta el evento, para que la imagen se dibuje pero no se vea
            _Gr.TranslateTransform(_xAumentaPos, _yAumentaPos)
            'Tambien aplicamos la rotación adecuada al objeto _Gr
            _Gr.RotateTransform(_AnguloGiro)
        End If
        'Dibujamos la imagen con las transformaciónes de ejes. 
        Dim RecEmpuja As New RectangleF(0, 0, _AnchoImagen, _AltoImagen)
        _Gr.Clear(_Color)
        _Gr.FillRectangle(_Brocha, RecEmpuja)
        'Refrescamos el contenedor de la imagen
        _Contenedor.Refresh()
        'Rotamos la imagen progresivamente
        _Gr.RotateTransform(_Aumento * intAngulo)
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Para asegurarnos de que la imagen de 1/2 vuelta, terminamos el efecto cuando _Contador = 90
        'o sea ...
        If _Contador = CInt(90 / intAngulo) + 1 Then
            'Restablecemos la imagen a su estado original antes de  descargar objetos
            If (Efecto = Efectos.Rodar_DAbajo) OrElse (Efecto = Efectos.Rodar_DArriba) _
            Then _bmpTextura.RotateFlip(RotateFlipType.RotateNoneFlipXY)
            DescargaObjetos()
        End If
    End Sub

    'Genera los efecto con múltiples Ejes
    Private Sub RuedaMultiple(ByVal Efecto As Efectos)

        Dim AnguloInicio, Paso, Ejes As Integer
        'Establecemos variables según la variante del efecto seleccionados
        Select Case Efecto
            Case Efectos.Rueda_2Ejes
                AnguloInicio = -90 : Paso = 180 : Ejes = 2
            Case Efectos.Rueda_3Ejes
                AnguloInicio = -90 : Paso = 120 : Ejes = 3
            Case Efectos.Rueda_4Ejes
                AnguloInicio = 0 : Paso = 90 : Ejes = 4
            Case Efectos.Rueda_8Ejes
                AnguloInicio = 0 : Paso = 45 : Ejes = 8
        End Select

        'La diagonal de la imágen va a ser el diametro. 
        'La calculamos por Pitagoras.
        Dim Diagonal As Single = CSng(Math.Pow((_AltoImagen ^ 2) + (_AnchoImagen ^ 2), 1 / 2))
        Dim i As Integer
        'Llenamos una torta con centro en el centro del Control,
        'con la textura de la imágen original,
        'agregandole trozos de _AnguloGiro
        Dim Rec As New Rectangle(CInt(-(Diagonal - _AnchoImagen) / 2), CInt(-(Diagonal - _AltoImagen) / 2), CInt(Diagonal), CInt(Diagonal))

        For i = 1 To Ejes
            _Gr.FillPie(_Brocha, Rec, AnguloInicio, _AnguloGiro)
            AnguloInicio += Paso
        Next
        _AnguloGiro += CSng(Paso / _Velocidad)
        'Mostramos los trozos actuales
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1
        'veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            DescargaObjetos()
        End If

    End Sub

    'Genera los efectos de Simetría
    Private Sub Simetrico(ByVal Efecto As Efectos)
        'La diagonal de la imágen va a ser el diametro. 
        'La calculamos por Pitagoras.
        Dim Diagonal As Single = CSng(Math.Pow((_AltoImagen ^ 2) + (_AnchoImagen ^ 2), 1 / 2))
        Dim AnguloInicio As Integer
        'Iniciamos variables según el efecto seleccionado
        If Efecto = Efectos.Simetrico_Adentro Then
            AnguloInicio = -90
        ElseIf Efecto = Efectos.Simetrico_Afuera Then
            AnguloInicio = 90
        ElseIf Efecto = Efectos.Simetrico_Izquierda Then
            AnguloInicio = 180
        ElseIf Efecto = Efectos.Simetrico_Derecha Then
            AnguloInicio = 0
        End If
        'Llenamos una torta con centro en el centro de _bmpDibujar,
        'con la textura de la imágen original,
        'agregandole trozos de _AnguloGiro
        Dim rec As New Rectangle(CInt(-(Diagonal - _AnchoImagen) / 2), CInt(-(Diagonal - _AltoImagen) / 2), CInt(Diagonal), CInt(Diagonal))

        _Gr.FillPie(_Brocha, rec, AnguloInicio, _AnguloGiro)
        _Gr.FillPie(_Brocha, rec, AnguloInicio, -_AnguloGiro)

        _AnguloGiro += CSng(180 / _Velocidad)
        'Mostramos los trozos actuales
        _Contenedor.Refresh()
        'Contamos las veces que se ejecutó este evento
        _Contador += 1
        'Cuando el evento se halla ejecutado _Velocidad + 1
        'veses terminamos el efecto
        If _Contador = _Velocidad + 1 Then
            DescargaObjetos()
        End If
    End Sub

    'Arreglo que se va a usar para contar los enteros pares generados por DameParUnico
    Private Rango() As Integer = {}
    'Genera y devuelve 'TotalCoordBarras' enteros positivos pares (de a uno) 
    'entre 0 y intMaximo (incluido el cero) de forma aleatoria y sin repetir 
    Private Function DameParUnico(ByVal intMaximo As Integer, ByVal TotalCoordBarras As Integer) As Integer
        'r: para generar los números de forma aleatoria
        Dim r As New Random
        'i: contiene cada número que se genera
        Dim i As Integer
        Static j As Integer = 0
        'Para saber si ya salio el cero 
        Static CeroYaSalio As Boolean = False

        If j = 0 Then
            'La primera vez redimensionamos el arreglo 'Rango' que contendrá los enteros
            ReDim Rango(TotalCoordBarras)
        ElseIf j = TotalCoordBarras Then
            'Cuando se llene el arreglo, reiniciamos variables y lo indicamos devolviendo -1
            j = 0
            CeroYaSalio = False
            Return -1
        End If

        Do
            i = r.Next(intMaximo)
            If i > 0 Then
                'Si i es par...
                If i Mod 2 = 0 Then
                    '... y no esta en el arreglo... 
                    If Rango.IndexOf(Rango, i) = -1 Then
                        '... lo agregamos en Rango y lo devolvemos
                        j += 1
                        Rango.SetValue(i, j - 1)
                        Return Rango(j - 1)
                    End If
                End If
            ElseIf Not CeroYaSalio Then
                'Agregamos el cero en el arreglo y lo devolvemos
                j += 1
                Rango.SetValue(i, j - 1)
                CeroYaSalio = True
                Return Rango(j - 1)
            End If
        Loop Until j = TotalCoordBarras

    End Function

#End Region

#End Region

#Region " Propiedades "
    'FALTA: Para controles grandes una propiedad para decidir como se va a dibujar la imagen.

    'EfectoActual
    <DefaultValue(GetType(Efectos), "Abanico_Derecha"), _
    Category("Comportamiento"), _
    Description("Establece el efecto actual para este objeto.")> _
    Public Property EfectoActual() As Efectos
        Get
            Return _Efecto
        End Get
        Set(ByVal Value As Efectos)
            'Si no se esta ejecutando un efecto en este momento cambiamos el efecto
            If Not Me.blnEfectoEjecutandose Then
                _Efecto = Value
            End If
        End Set
    End Property
    'VelocidadEfecto
    <DefaultValue(GetType(Integer), "20"), _
    Category("Comportamiento"), _
    Description("Establece la velocidad de dibujado para este objeto.")> _
    Public Property VelocidadEfecto() As Integer
        Get
            Return _Velocidad
        End Get
        Set(ByVal Value As Integer)
            _Velocidad = ValidaVelocidad(Value)
        End Set
    End Property
    'ColorTrans
    <DefaultValue(GetType(Color), "Transparent"), _
    Category("Apariencia"), _
    Description("Establece el color de relleno para este objeto.")> _
    Public Property ColorTrans() As Color
        Get
            Return _Color
        End Get
        Set(ByVal Value As Color)
            _Color = Value
        End Set
    End Property
    'Image
    <Category("Apariencia"), _
    Description("Establece la imagen que se usará para realizar el efecto seleccionado.")> _
    Public Property Image() As Image
        Get
            Return _bmpTextura
        End Get
        Set(ByVal Value As Image)
            Try
                _bmpTextura = Value
                _AnchoImagen = _bmpTextura.Width
                _AltoImagen = _bmpTextura.Height
                'Hay imagenes que tienen un formato de pixel
                'que no permite crear un objeto Graphics para dibujar sobre
                'ellas, por eso creamos una copia de la imagen, del mismo tamaño
                'y con un formato de pixel que no de problemas
                _bmpDibujar = New Bitmap(CInt(_AnchoImagen), CInt(_AltoImagen), PixelFormat.Format32bppArgb)
            Catch ex As Exception When Value Is Nothing
                Throw New NullReferenceException("La imagen no puede ser nula.")
            End Try
        End Set
    End Property
#End Region

#Region " Destructores "

    Public Overloads Sub Dispose() Implements System.IDisposable.Dispose
        Dispose(True)
        'GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                'Acá, limpiamos recursos administrados de esta clase
                'En este caso no hay ningúno (que no haya sido descargado)
            End If
            'Acá, limpiamos recursos no administrados de esta clase
            If Not _bmpTextura Is Nothing Then
                _bmpTextura.Dispose()
            End If
            _bmpDibujar.Dispose()
            Me.disposed = True
        End If
    End Sub

    ' Este metodo Finalize se ejecutará (automaticamente) solamente
    ' si el metodo Dispose no es usado por el usuario.
    Protected Overrides Sub Finalize()
        'Limpiamos solamente los recursos no manejados pasando False
        Dispose(False)
    End Sub

#End Region

End Class