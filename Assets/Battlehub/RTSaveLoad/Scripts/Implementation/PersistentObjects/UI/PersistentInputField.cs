#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects.UI
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentInputField

#if RT_PE_MAINTANANCE
        : PersistentData
#else
        : Battlehub.RTSaveLoad.PersistentObjects.UI.PersistentSelectable
#endif
    {
        public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.UI.InputField o = (UnityEngine.UI.InputField)obj;
			o.shouldHideMobileInput = shouldHideMobileInput;
			o.text = text;
			o.caretBlinkRate = caretBlinkRate;
			o.caretWidth = caretWidth;
			o.textComponent = (UnityEngine.UI.Text)objects.Get(textComponent);
			o.placeholder = (UnityEngine.UI.Graphic)objects.Get(placeholder);
			o.caretColor = caretColor;
			o.customCaretColor = customCaretColor;
			o.selectionColor = selectionColor;

            Write(o.onEndEdit, onEndEdit, objects);
            Write(o.onValueChanged, onValueChanged, objects);
            
			o.onValidateInput = onValidateInput;
			o.characterLimit = characterLimit;
			o.contentType = contentType;
			o.lineType = lineType;
			o.inputType = inputType;
			o.keyboardType = keyboardType;
			o.characterValidation = characterValidation;
			o.readOnly = readOnly;
			o.asteriskChar = asteriskChar;
			o.caretPosition = caretPosition;
			o.selectionAnchorPosition = selectionAnchorPosition;
			o.selectionFocusPosition = selectionFocusPosition;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.InputField o = (UnityEngine.UI.InputField)obj;
			shouldHideMobileInput = o.shouldHideMobileInput;
			text = o.text;
			caretBlinkRate = o.caretBlinkRate;
			caretWidth = o.caretWidth;
			textComponent = o.textComponent.GetMappedInstanceID();
			placeholder = o.placeholder.GetMappedInstanceID();
			caretColor = o.caretColor;
			customCaretColor = o.customCaretColor;
			selectionColor = o.selectionColor;

            Read(onEndEdit, o.onEndEdit);
            Read(onValueChanged, o.onValueChanged);

            onValidateInput = o.onValidateInput;
			characterLimit = o.characterLimit;
			contentType = o.contentType;
			lineType = o.lineType;
			inputType = o.inputType;
			keyboardType = o.keyboardType;
			characterValidation = o.characterValidation;
			readOnly = o.readOnly;
			asteriskChar = o.asteriskChar;
			caretPosition = o.caretPosition;
			selectionAnchorPosition = o.selectionAnchorPosition;
			selectionFocusPosition = o.selectionFocusPosition;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(textComponent, dependencies, objects, allowNulls);
			AddDependency(placeholder, dependencies, objects, allowNulls);

            if(onEndEdit != null)
            {
                onEndEdit.FindDependencies(dependencies, objects, allowNulls);
            }

            if(onValueChanged != null)
            {
                onValueChanged.FindDependencies(dependencies, objects, allowNulls);
            }
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.UI.InputField o = (UnityEngine.UI.InputField)obj;
			AddDependency(o.textComponent, dependencies);
			AddDependency(o.placeholder, dependencies);

            PersistentUnityEventBase evnt = new PersistentUnityEventBase();
            evnt.GetDependencies(o.onEndEdit, dependencies);
            evnt.GetDependencies(o.onValueChanged, dependencies);
        }

        public bool shouldHideMobileInput;

        public string text;

        public float caretBlinkRate;

        public int caretWidth;

        public long textComponent;

        public long placeholder;

        public UnityEngine.Color caretColor;

        public bool customCaretColor;

        public UnityEngine.Color selectionColor;

        public PersistentUnityEventBase onEndEdit;

        public PersistentUnityEventBase onValueChanged;

        public UnityEngine.UI.InputField.OnValidateInput onValidateInput;

        public int characterLimit;

        public UnityEngine.UI.InputField.ContentType contentType;
        		
        public UnityEngine.UI.InputField.LineType lineType;

        public UnityEngine.UI.InputField.InputType inputType;

        public UnityEngine.TouchScreenKeyboardType keyboardType;

        public UnityEngine.UI.InputField.CharacterValidation characterValidation;

        public bool readOnly;

        public char asteriskChar;

        public int caretPosition;

        public int selectionAnchorPosition;

        public int selectionFocusPosition;

	}
}
